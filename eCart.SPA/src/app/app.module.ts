import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';

@NgModule({
    declarations: [
        AppComponent,
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        CoreModule,
        HomeModule], providers: [
            { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
            { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
            { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
            provideHttpClient(withInterceptorsFromDi())
        ]
})
export class AppModule { }
