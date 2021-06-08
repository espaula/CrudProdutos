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
  foto!: File; 

  constructor(private produtoService: ProdutosService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.erros = [];
    this.formulario = new FormGroup({

      nomedoProduto: new FormControl(null,[Validators.required, Validators.maxLength(50)]),
      valordeVenda: new FormControl(null, [Validators.required]),
      foto: new FormControl(null, [Validators.required])

    });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  EnviarFormulario(): void{

    const produto = this.formulario.value;
    const formData: FormData = new FormData();
    this.erros = [];
    produto["usuarioId"] = localStorage.getItem('UsuarioId');

    if(this.foto != null){

      formData.append('file', this.foto, this.foto.name);
    }

    this.produtoService.SalvarFoto(formData).subscribe(resultado =>{

      produto.foto = resultado.foto;

    // console.log(resultado);

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


    });

    
   
  }

  
VoltarListagem(): void{
  this.router.navigate(['produtos/listagemProdutos']);
}

SelecionarFoto(fileInput: any):void{

  this.foto = fileInput.target.files[0] as File;
  const reader = new FileReader();
  reader.onload = function(e: any){
    document.getElementById('foto')?.removeAttribute('hidden');
    document.getElementById('foto')?.setAttribute('src', e.target.result);
  }

  reader.readAsDataURL(this.foto);

}

}
