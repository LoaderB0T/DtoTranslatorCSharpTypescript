export class Vehicle {
  public personCount: number;
  public mainColor: string;
  public brand: Brand;
}

export class Aircraft extends Vehicle {
  public maxFlightHeight: number;
}

export enum Brand {
  GOOD_BRAND = 0,
  BAD_BRAND = 1,
  BASIC_BRAND = 2
}

export class Car extends Vehicle {
  public seatWarmers: boolean;
}

export class Cabrio extends Car {
  public canBeOpenedWhileDriving: boolean;
}

export class Chopper extends Aircraft {
  public chopperId: string;
}

export class Plane extends Aircraft {
  public planeId: string;
}

export class RoadVehicle extends Vehicle {
  public wheelCount: number;
}

export class Truck extends RoadVehicle {
  public maxCargoSize: number;
}
