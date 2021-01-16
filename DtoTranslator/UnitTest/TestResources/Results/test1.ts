export class TestADto {
  public propA: string;
  public propB: boolean;
  public propC: boolean;
}

export class TestBDto {
  public propA: string[];
  public propB?: boolean;
}

export class TestCDto {
  public propA: number;
  public propB: number;
  public propC: number;
  public propD: number;
  public propE: boolean;
  public propF: string;
}

export class TestDDto {
  public propA: TestEA;
  public propB: TestEB;
  public propC: TestEC;
}

export class TestEDto {
  public propA: string;
  public propB: boolean;
  public propC: boolean;
  public propD: TestEA;
  public propE: TestEB;
  public propF: TestEC;
}

export class TestFDto {
  public propA: Array<string>;
  public propB: Array<boolean>;
  public propC: boolean[];
}

export class TestGDto {
  public propA: Dictionary<string, number>;
  public propB: Array<Array<number>>;
  public propC: Array<Array<Dictionary<string, Array<number>>>>;
  public propD: Tuple<string, number, boolean, number>;
}

export enum TestEA {
  ENUM_VAL_4 = 4,
  ENUM_VAL_5 = 5,
  ENUM_VAL_7 = 7
}

export enum TestEB {
  ENUM_VAL_0 = 0,
  ENUM_VAL_1 = 1,
  ENUM_VAL_2 = 3,
  ENUM_VAL_3 = 4,
  ENUM_VAL_4 = 5,
  ENUM_VAL_5 = 6
}

export enum TestEC {
  ENUM_VAL_0 = 0,
  ENUM_VAL_1 = 1,
  ENUM_VAL_2 = 3,
  ENUM_VAL_3 = 4
}
