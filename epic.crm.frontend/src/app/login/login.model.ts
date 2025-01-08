export class LoginDto {
  UserName!: string;
  Password!: string;
}

export class LoggedUserDto {
  name!: string;
  email!: string;
  isAdmin!: boolean;
}
