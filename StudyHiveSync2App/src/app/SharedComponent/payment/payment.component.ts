// import {
//   Component,
//   OnInit
// } from '@angular/core';
// import {
//   IPayPalConfig,
//   ICreateOrderRequest,NgxPayPalModule 
// } from 'ngx-paypal';
import { Component, OnInit } from '@angular/core';
import { IPayPalConfig, ICreateOrderRequest, NgxPayPalModule } from 'ngx-paypal';
import { Router } from '@angular/router';


@Component({
  imports: [NgxPayPalModule],
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {

  public payPalConfig ? : IPayPalConfig;

  //new below line
  constructor(private router: Router) {}

  ngOnInit(): void {
      this.initConfig();
  }

  private initConfig(): void {
      this.payPalConfig = {
          currency: 'USD',
          clientId: 'Aao6KmLr-LiobEIfMVLlakV9ZMT2yz7B8eHELW6edIRRz4pQa5fabiD61NJjyks2MgbgEfK7jnU5Edtj',
          createOrderOnClient: (data) => < ICreateOrderRequest > {
              intent: 'CAPTURE',
              purchase_units: [{
                  amount: {
                      currency_code: 'USD',
                      value: '99',
                      breakdown: {
                          item_total: {
                              currency_code: 'USD',
                              value: '99'
                          }
                      }
                  },
                  items: [{
                      name: 'Enterprise Subscription',
                      quantity: '1',
                      category: 'DIGITAL_GOODS',
                      unit_amount: {
                          currency_code: 'USD',
                          value: '99',
                      },
                  }]
              }]
          },
        //   advanced: {
        //       commit: 'true'
        //   },
        //   style: {
        //       label: 'paypal',
        //       layout: 'vertical'
        //   },
          onApprove: (data, actions) => {
              console.log('onApprove - transaction was approved, but not authorized', data, actions);
              actions.order.get().then((details:any) => {
                  console.log('onApprove - you can get full order details inside onApprove: ', details);
              });

          },
          onClientAuthorization: (data) => {
              console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
              // Redirect to ThankYouComponent with orderId and paymentId
        const orderId = data.id; // PayPal order ID
        // const paymentId = data.purchase_units[0]?.payments?.captures[0]?.id || 'N/A'; // PayPal payment ID
        this.router.navigate(['/thank-you'], {
          queryParams: { orderId}
        });
                      },
          onCancel: (data, actions) => {
              console.log('OnCancel', data, actions);

          },
          onError: err => {
              console.log('OnError', err);
          },
          onClick: (data, actions) => {
              console.log('onClick', data, actions);
              return false;
          }
      };
  }
}


// import { Component } from '@angular/core';
// import { NgxPayPalModule } from 'ngx-paypal';
// @Component({
//   selector: 'app-payment',
//   imports: [],
//   templateUrl: './payment.component.html',
//   styleUrl: './payment.component.css'
// })
// @NgModule({
//   imports: [
//     NgxPayPalModule,
//     ...
//   ],
// })
// export class PaymentComponent {

// }
