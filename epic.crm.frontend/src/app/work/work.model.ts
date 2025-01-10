import { Address, Customer } from "../customers/customers.model";
import { AppUser } from "../user/user.model";

export class Work {
  workId!: number;
  name!: string;
  customerId!: number | null;
  workDateTime!: string | null;
  description!: string;
  addressId!: number | null;
  workStatusId!: number | null;
  appUserId!: number;
  price!: number | null;
  address!: Address;
  appUser!: AppUser;
  customer!: Customer;
  workStatus!: WorkDto;
}

export class WorkDto extends Work {
  customerName!: string;
  addressLiteral!: string;
}

export class WorkStatusDto {
  workStatusId!: number;
  name!: string;
  work!: Work[];
}

export class WorkEditRegisterDto extends Work {
  zipCode!: number;
  city!: string;
  houseAddress!: string;
}

export class CalendarDto {
  date!: string;
  title!: string;
  description!: string;
}

export enum WorkStatusEnum {
  Planned = 1,
  InProgress = 2,
  Done = 3,
  Deleted = 4
}
