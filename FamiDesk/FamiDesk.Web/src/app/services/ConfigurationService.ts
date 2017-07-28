import {Injectable} from '@angular/core';

@Injectable()
export abstract class ConfigurationService {
  public abstract get serverBaseUrl(): string;
  public abstract get pollingMs(): number;
}

@Injectable()
export class ConfigurationServiceLocal extends ConfigurationService {
  public get serverBaseUrl(): string {
    //   return 'http://localhost:51799/tables/';
    return 'http://localhost:4200/tables/';
  }

  public get pollingMs(): number{
      return 5000;
  }
}
@Injectable()
export class ConfigurationServiceProd extends ConfigurationService {
  public get serverBaseUrl(): string {
      return 'http://famideskmobileapp.azurewebsites.net/tables/';
  }
    
  public get pollingMs(): number{
      return 5000;
  }
}