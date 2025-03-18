import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
// import { provideRouter } from '@angular/router';
// import { provideHttpClient } from '@angular/common/http';
// import { routes } from './app/app.routes';
// import { importProvidersFrom } from '@angular/core';
// import { FormsModule } from '@angular/forms';
// import { provideAnimations } from '@angular/platform-browser/animations';


// bootstrapApplication(AppComponent, {
//   providers: [
//     provideRouter(routes),
//     provideHttpClient(),
//     provideAnimations(),
//     importProvidersFrom(FormsModule)
//   ]
// }).catch(err => console.error(err));

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
