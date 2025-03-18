import { HttpInterceptorFn } from '@angular/common/http';            //This type is used to define an HTTP interceptor function.

//This line defines and exports a constant named authInterceptor which is an HTTP interceptor function. The function takes two parameters:
                                                                                //req: The outgoing HTTP request.
                                                                                //next: A function that forwards the request to the next interceptor in the chain.
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('jwtToken');                               //This line retrieves the JWT token from local storage.
 
  if(token) {                                                             //This line checks if the token is present.
    const cloneReq = req.clone({                                          //This line creates a clone of the request with the Authorization header set to the JWT token.
      setHeaders: {
        Authorization: `Bearer ${token}`                                  //The Authorization header is formatted as Bearer <token>.
      }
    });
 
    return next(cloneReq);                                                //This line forwards the cloned request to the next interceptor in the chain.
  }
  return next(req);                                                       //If the token is not present, the original request is forwarded to the next interceptor.
};
