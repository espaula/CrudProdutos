import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Produtos } from 'src/app/models/Produtos';
import { ProdutosService } from 'src/app/services/produtos.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-atualizarproduto',
  templateUrl: './atualizarproduto.component.html',
  styleUrls: ['../listagem-produtos/listagem-produtos.component.css']
})
export class AtualizarprodutoComponent implements OnInit {


nomeProduto!: string;
produtoId!: number;
produto!: Observable<Produtos>;
formulario: any;
erros: string [] = [];
urlFoto!: SafeResourceUrl;
foto!: File; 
fotoAnterior!: File; 



  constructor(private router: Router, 
    private route: ActivatedRoute,
    private produtoService: ProdutosService,
    private snackBar: MatSnackBar,
    private sanitizer: DomSanitizer
    ) { }

  ngOnInit(): void {

    this.produtoId = this.route.snapshot.params.id;

    
    this.produtoService.RetornarFotoProduto(this.produtoId.toString()).subscribe(resultado =>{

      this.urlFoto = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'+resultado.imagem);
      this.fotoAnterior = resultado.imagem;


    });

      this.produtoService.PegarProdutoPeloId(this.produtoId).subscribe(resultado =>{
          
      //  this.valordeVenda = resultado.valordeVenda;
        this.nomeProduto = resultado.nomedoProduto;

        this.formulario = new FormGroup({
            produtoId: new FormControl(resultado.produtoId),
            nomedoProduto: new FormControl(resultado.nomedoProduto,[Validators.required, Validators.maxLength(50)]),
            usuarioId: new FormControl(resultado.usuarioId),
            foto: new FormControl(null),
            valordeVenda: new FormControl(resultado.valordeVenda,[Validators.required]),

          });

      });
  }


get propriedade(){
  return this.formulario.controls;
}


EnviarFormulario():void{

  const formData: FormData = new FormData();
  const produto = this.formulario.value;
  this.erros = [];

  if(this.foto != null){

    formData.append('file', this.foto, this.foto.name);
  }
  this.produtoService.SalvarFoto(formData).subscribe(resultado =>{

    produto.foto = resultado.foto;


  this.produtoService.AtualizarProdutos(this.produtoId, produto)
  .subscribe(resultado => {
        this.router.navigate(['produtos/listagemProdutos']);
        this.snackBar.open(resultado.mensagem, undefined ,{
          duration:2000,
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
        
      },
      (err)=>{

        if(err.status ===400){
            for(const campo in err.error.errors){
              this.erros.push(err.error.errors[campo]);
              
            }
          
        }
      }

  );

 


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
