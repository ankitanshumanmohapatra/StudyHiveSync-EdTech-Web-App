import { Routes } from '@angular/router';
import { SignupComponent } from './SharedComponent/signup/signup.component';
import { LoginComponent } from './SharedComponent/login/login.component';
import { HomePageComponent } from './SharedComponent/home-page/home-page.component';
import { AboutUsComponent } from './SharedComponent/about-us/about-us.component';
import { CategoryComponent } from './SharedComponent/category/category.component';
import { DashboardComponent } from './SharedComponent/dashboard/dashboard.component';
import { CoursesComponent } from './SharedComponent/courses/courses.component';
import { PricingComponent } from './SharedComponent/pricing/pricing.component';
import { NavBarComponent } from './SharedComponent/nav-bar/nav-bar.component';
import { SectionComponent } from './SharedComponent/section/section.component';
import { PaymentComponent } from './SharedComponent/payment/payment.component';
import { ThankYouComponent } from './SharedComponent/thank-you/thank-you.component';

// export const routes: Routes = [];



export const routes: Routes = [
    { path: "signup", component: SignupComponent },
    { path: "", redirectTo: "/signup", pathMatch: "full" },
    { path: "login", component: LoginComponent },
    { path: "home", component: HomePageComponent },
    { path: "category", component: CategoryComponent },
    { path: "about-us", component: AboutUsComponent },
    { path: "dashboard", component: DashboardComponent },
    { path: 'course/:id', component: CoursesComponent },
    { path: 'pricing', component: PricingComponent },
    // { path: 'section', component: SectionComponent}
    { path: 'payment', component: PaymentComponent},
    { path: 'thank-you', component: ThankYouComponent}

];
