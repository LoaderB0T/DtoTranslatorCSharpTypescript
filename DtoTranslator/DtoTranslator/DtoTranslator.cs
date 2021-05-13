using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtoTranslator
{
    public static class DtoTranslator
    {
        static string newLine = "\r\n";

        public static List<ObjectModel> GetObjectModelFromDlls(string[] dllPaths, string namespaceFilter = null)
        {
            var objectModels = new List<ObjectModel>();
            dllPaths.ToList().ForEach(dllPath =>
            {
                ModuleContext modCtx = ModuleDef.CreateModuleContext();
                ModuleDefMD module = ModuleDefMD.Load(dllPath, modCtx);
                var allTypes = module.Types;

                IEnumerable<TypeDef> dtoTypes;
                if (!string.IsNullOrEmpty(namespaceFilter))
                {
                    dtoTypes = allTypes.Where(x => x.Namespace.String.ToLower().Contains("dto") && x.Namespace.String.ToLower().Contains(namespaceFilter));
                }
                else
                {
                    dtoTypes = allTypes.Where(x => x.Namespace.String.ToLower().Contains("dto"));
                }



                foreach (var dto in dtoTypes)
                {
                    if (dto.IsEnum)
                    {
                        var newEnum = new EnumModel(dto.Name);
                        var fields = dto.Fields.Skip(1);
                        foreach (var field in fields)
                        {
                            var enumValue = new EnumValueModel();
                            enumValue.Index = (int)field.Constant.Value;
                            enumValue.Name = field.Name;
                            newEnum.Values.Add(enumValue);
                        }
                        objectModels.Add(newEnum);
                    }
                    else if (dto.IsClass)
                    {
                        var newClass = new ClassModel(dto.Name);

                        if (dto.BaseType.ReflectionName != "Object")
                        {
                            newClass.ParentClass = dto.BaseType.ReflectionName;
                        }

                        var props = dto.Properties;
                        foreach (var prop in props)
                        {
                            var classProp = new PropertyModel();
                            classProp.Name = prop.Name;

                            var type = prop.Type as PropertySig;
                            var retType = type.RetType;

                            var typeModel = TranslateType(retType, classProp);
                            classProp.Type = typeModel;
                            newClass.Props.Add(classProp);
                        }
                        objectModels.Add(newClass);
                    }
                }
                module.Dispose();
            });
            return objectModels;
        }

        private static TypeModel TranslateType(TypeSig retType, PropertyModel propertyModel)
        {
            var classType = new TypeModel();

            var ownTypeName = retType.ReflectionName.Split("`")[0];
            classType.TypeName = ownTypeName;

            if (retType.IsGenericInstanceType)
            {
                var genericType = retType as GenericInstSig;
                var genericArgs = genericType.GenericArguments;

                if (ownTypeName != "Nullable")
                {
                    classType.ChildTypes = new List<TypeModel>();

                    foreach (var genericArg in genericArgs)
                    {
                        var genericArgType = TranslateType(genericArg, propertyModel);
                        classType.ChildTypes.Add(genericArgType);
                    }
                }
                else
                {
                    classType = TranslateType(genericArgs[0], propertyModel);
                    propertyModel.IsNullable = true;
                }
            }

            return classType;
        }

        public static string TranslateObjectModelToTypescript(List<ObjectModel> objModels)
        {
            objModels = SortClassModels(objModels);

            StringBuilder returnStringBuilder = new StringBuilder();

            foreach (var objModel in objModels)
            {
                if (returnStringBuilder.Length > 0)
                {
                    returnStringBuilder.Append(newLine);
                }
                if (objModel is ClassModel classModel)
                {
                    var parentClassString = "";
                    if (!string.IsNullOrWhiteSpace(classModel.ParentClass))
                    {
                        parentClassString = " extends " + classModel.ParentClass;
                    }
                    returnStringBuilder.Append("export interface " + objModel.Name + parentClassString + " {" + newLine);
                    classModel.Props.ForEach(x =>
                    {
                        returnStringBuilder.Append(x.GetString(newLine));
                    });
                }
                else if (objModel is EnumModel enumModel)
                {
                    returnStringBuilder.Append("export enum " + objModel.Name + " {" + newLine);
                    int i = 0;
                    enumModel.Values.ForEach(x =>
                    {
                        i++;
                        returnStringBuilder.Append(x.GetString(newLine, i == enumModel.Values.Count));
                    });
                }
                else
                {
                    throw new InvalidOperationException("Other types are not implemented");
                }
                returnStringBuilder.Append("}" + newLine);
            }

            return returnStringBuilder.ToString();
        }

        private static void AddSingleModelToSortedList(List<ObjectModel> sortedModels, ObjectModel modelToAdd, List<ObjectModel> allModels)
        {
            if (sortedModels.Contains(modelToAdd))
            {
                return;
            }

            if (modelToAdd is ClassModel classModelToAdd)
            {
                if (!string.IsNullOrEmpty(classModelToAdd.ParentClass))
                {
                    if (!sortedModels.Any(x => x.Name == classModelToAdd.ParentClass))
                    {
                        AddSingleModelToSortedList(sortedModels, allModels.Find(x => x.Name == classModelToAdd.ParentClass), allModels);
                        AddSingleModelToSortedList(sortedModels, modelToAdd, allModels);
                    }
                    else
                    {
                        sortedModels.Add(classModelToAdd);
                    }
                }
                else
                {
                    sortedModels.Add(classModelToAdd);
                }
            }
            else
            {
                sortedModels.Add(modelToAdd);
            }
        }


        private static List<ObjectModel> SortClassModels(List<ObjectModel> objModels)
        {
            var sortedObjModels = new List<ObjectModel>();
            objModels.ForEach(x =>
            {
                AddSingleModelToSortedList(sortedObjModels, x, objModels);
            });

            return sortedObjModels;
        }
    }
}
