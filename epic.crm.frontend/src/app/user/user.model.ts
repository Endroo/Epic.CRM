import { Customer } from "../customers/customers.model";
import { Work } from "../work/work.model";

export class AppUser {
  appUserId!: number;
  name!: string;
  email!: string;
  profession!: string;
  isAdmin!: boolean;
  aspNetUserId!: string;
  customer!: Customer[];
  work!: Work[];
}


export class AppUserDto extends AppUser {
  workCount!: number;
  customerCount!: number;
}
