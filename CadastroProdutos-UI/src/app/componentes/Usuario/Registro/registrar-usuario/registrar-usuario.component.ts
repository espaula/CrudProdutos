import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent implements OnInit {

  formulario:any;
  foto!: File;
  erros:string[]=[];

  constructor(private usuarioService: UsuarioService,
    private router: Router) { }

  ngOnInit(): void {

    this.erros =[];
    this.formulario = new FormGroup({

        nomeusuario: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)]),
        cpf: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
        profissao: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(30)]),
        foto: new FormControl(null, [Validators.required]),
        senha: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)])

    });
  }

  get propriedade(){
    return this.formulario.controls;

  }

}
