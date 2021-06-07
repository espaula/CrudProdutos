import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-login-usuario',
  templateUrl: './login-usuario.component.html',
  styleUrls: ['./login-usuario.component.css']
})
export class LoginUsuarioComponent implements OnInit {



  formulario: any;
  erros: string[]=[];

  constructor(private usuariosService: UsuarioService,
    private router: Router) { }

  ngOnInit(): void {

    this.erros =[];
    this.formulario = new FormGroup({
        login: new FormControl(null, [Validators.required, Validators.minLength(1)]),
        senha: new FormControl(null, [Validators.required, Validators.minLength(1)])
    });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  EnviarFormulario():void{
   
   
    this.erros=[];
    const dadosLogin = this.formulario.value;

    this.usuariosService.LogarUsuario(dadosLogin).subscribe(resultado =>{

      const LoginUsuario = resultado.loginUsuarioLogado;
      const usuarioId = resultado.usuarioId;
      const tokenUsuario = resultado.tokenUsuarioLogado;

      localStorage.setItem("LoginUsuarioLogado", LoginUsuario);
      localStorage.setItem("UsuarioId",usuarioId);
      localStorage.setItem("TokenUsuarioLogado",tokenUsuario);

      this.router.navigate(['/produtos/listagemProdutos'])

    },(err)=>{

      if(err.status ===400){
        for(const campo in err.error.errors){
          if(err.error.errors.hasOwnProperty(campo)){
            this.erros.push(err.error.errors[campo])
          }
        }
     }
     else{
       this.erros.push(err.error);
     }

    }
    );

  }

}
