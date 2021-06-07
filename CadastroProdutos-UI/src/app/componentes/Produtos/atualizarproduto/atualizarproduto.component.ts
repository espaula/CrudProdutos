import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Produtos } from 'src/app/models/Produtos';
import { ProdutosService } from 'src/app/services/produtos.service';
import {MatSnackBar} from '@angular/material/snack-bar';

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



  constructor(private router: Router, 
    private route: ActivatedRoute,
    private produtoService: ProdutosService,
    private snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {

      this.produtoId = this.route.snapshot.params.id;
      this.produtoService.PegarProdutoPeloId(this.produtoId).subscribe(resultado =>{
          
      //  this.valordeVenda = resultado.valordeVenda;
        this.nomeProduto = resultado.nomedoProduto;

        this.formulario = new FormGroup({
            produtoId: new FormControl(resultado.produtoId),
            nomedoProduto: new FormControl(resultado.nomedoProduto,[Validators.required, Validators.maxLength(50)]),
            usuarioId: new FormControl(resultado.usuarioId),
            imagem: new FormControl(resultado.imagem,  [Validators.required,Validators.maxLength(15)]),
            valordeVenda: new FormControl(resultado.valordeVenda,[Validators.required]),

          });

      });
  }


get propriedade(){
  return this.formulario.controls;
}


EnviarFormulario():void{

  const produto = this.formulario.value;
  this.erros = [];
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

}

VoltarListagem(): void{
  this.router.navigate(['produtos/listagemProdutos']);
}
}
