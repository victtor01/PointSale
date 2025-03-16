import { IEmployee } from "./IEmployee";

export interface IPermissionInfo {
  enumName: string;
  description: string;
  name: string;
  employees?: IEmployee[];
}
