import { PipeTransform, Pipe } from "@angular/core";

@Pipe({ name: 'enumToArray' })
export class EnumToArrayPipe implements PipeTransform {
  //transform(value: any): Object {
  //  return Object.values(value).filter(
  //    (type) => isNaN(<any>type) && type !== 'values'
  //  );
  //}

  transform(value: any): any {
    if (value === null || value === undefined) {
      return null;
    }
    return Object.keys(value).filter(e => !isNaN(+e)).map(o => { return { index: +o, name: value[o] } });
  }
}


