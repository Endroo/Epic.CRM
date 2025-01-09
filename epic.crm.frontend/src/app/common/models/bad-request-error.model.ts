export interface BadRequestError {
  errorLabel: string;
  error: BadRequestErrorParameters[];
}

export interface BadRequestErrorParameters {
  key: string;
  value: string;
}
