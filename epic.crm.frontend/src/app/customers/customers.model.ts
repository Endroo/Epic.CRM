import { AppUser } from "../user/user.model";
import { Work } from "../work/work.model";

export class Customer {
  customerId!: number;
  name!: string;
  email!: string;
  addressId!: number | null;
  appUserId!: number;
  address!: Address;
  appUser!: AppUser;
  work!: Work[];
}

export class CustomerDto extends Customer {
  addressLiteral!: string;
}

export class CustomerEditRegisterDto extends Customer {
  zipCode!: number;
  city!: string;
  houseAddress!: string;
}


export class Address {
  addressId!: number;
  zipCode!: number | null;
  city!: string;
  houseAddress!: string;
  customer!: Customer[];
  work!: Work[];
}
