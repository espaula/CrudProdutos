import { Injectable } from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import jwtDecode from 'jwt-decode';
import decode from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private jwtHelper: JwtHelperService,
    private router:Router ) { }

    canActivate(): boolean{

      const token = localStorage.getItem("TokenUsuarioLogado");

      if(token && !this.jwtHelper.isTokenExpired(token)){
          return true;
      }
      this.router.navigate(['usuario/loginusuario']);

      return false;
    }

    VerificarAdministrador():boolean{

      const token = (localStorage.getItem("TokenUsuarioLogado") || '{}');
      const decodedToken = jwtDecode<MyToken>(token);

      if(decodedToken.role === 'Administrador'){
          return true;
      }
      else{
        return false;
      }


    }

   
}
interface MyToken {
  role: string;
  exp: number;
  // whatever else is in the JWT.
}
