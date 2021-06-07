import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
 styleUrls: ['../../DashBoard//dashboard/dashboard.component.css']

})
export class HeaderComponent implements OnInit {


  loginUsuarioLogado = localStorage.getItem('LoginUsuarioLogado');
  constructor(private router:Router) { }

  ngOnInit(): void {
  }

EfetuarLogout():void{

  localStorage.clear();

  this.router.navigate(['/usuario/loginusuario']);

}

}
