import { Pipe, PipeTransform } from '@angular/core';
import User from '../models/user';

@Pipe({
  name: 'searchFilterUser'
})
export class SearchFilterUserPipe implements PipeTransform {

  transform(users: User[], searchTerm: string): User[] {
    if (!users || !searchTerm) {
      return users;
    }

    searchTerm = searchTerm.toLowerCase();
    return users.filter(user => {
      return (
        user.firstName.toLowerCase().includes(searchTerm) ||
        user.lastName.toString().includes(searchTerm)
      );
    });
  }

}
