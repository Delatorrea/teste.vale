import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public addreses: Address[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Address>(baseUrl + 'company/taxidentifier/1').subscribe(result => {
      this.addreses.push(result);
    }, error => console.error(error));
  }
}

interface Address {
  TaxIdentifier: string;
}
