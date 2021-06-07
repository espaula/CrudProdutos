import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl, Validators} from  '@angular/forms';
import { Router } from '@angular/router';
import { ProdutosService } from 'src/app/services/produtos.service';
import {MatSnackBar} from '@angular/material/snack-bar'


@Component({
  selector: 'app-novo-produto',
  templateUrl: './novo-produto.component.html',
//  styleUrls: ['./novo-produto.component.css']
  styleUrls: ['../listagem-produtos/listagem-produtos.component.css']
})
export class NovoProdutoComponent implements OnInit {

  formulario:any;
  erros:string [] =[];

  constructor(private produtoService: ProdutosService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.erros = [];

    this.formulario = new FormGroup({

      nomedoProduto: new FormControl(null,[Validators.required, Validators.maxLength(50)]),
      valordeVenda: new FormControl(null, [Validators.required]),
      imagem: new FormControl(null, [Validators.required,Validators.maxLength(15)])

    });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  EnviarFormulario(): void{

    const produto = this.formulario.value;

    this.erros = [];

    produto["usuarioId"] = localStorage.getItem('UsuarioId');

    this.produtoService.NovoProduto(produto).subscribe(resultado=>{

      this.router.navigate(['/produtos/listagemProdutos']);
      this.snackBar.open(resultado.mensagem, undefined ,{
        duration:2000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });

    },(err)=>{

      if(err.status ===400){
          for(const campo in err.error.errors){
          //  if(err.error.errors.hasOwnProperty(campo)){
              this.erros.push(err.error.errors[campo])
          //  }
            
          }

      }
    });
  }

  
VoltarListagem(): void{
  this.router.navigate(['produtos/listagemProdutos'])
}

}
