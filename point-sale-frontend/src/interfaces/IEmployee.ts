export interface IEmployee {
  id: string;
  username: string;
  firstName: string;
  lastName?: string;
  email?: string;
  phone?: string;
  salary?: string;
  birthDate?: string | Date;
  createdAt: string;

  positions: IPositionEmployee[];
}

export interface IPositionEmployee {
  id: string;
  name: string;
  permissions: string[];
  employees?: IEmployee[];
}
