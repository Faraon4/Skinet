import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,    //  Instead of putting here all modules that we create, we just put a file where we put all modules
    ShopModule, // Module created by us, such way , HTML sees that we have this module and will have the necesary information
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
