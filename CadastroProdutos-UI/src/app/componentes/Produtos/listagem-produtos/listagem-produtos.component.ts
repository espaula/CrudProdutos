import { Component, Inject, OnInit, ViewChild } from '@angular/core';

import {ProdutosService} from './../../../services/produtos.service';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import {startWith, map} from 'rxjs/operators';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-listagem-produtos',
  templateUrl: './listagem-produtos.component.html',
  styleUrls: ['./listagem-produtos.component.css']
})
export class ListagemProdutosComponent implements OnInit {

produtos = new MatTableDataSource<any>();
displayedColumns: string[]=[];
//autocompleteInput = new FormControl();
autocompleteInput = new FormControl();
opcoesProdutos:string[]=[];
nomeProdutos!: Observable<String[]>;




@ViewChild(MatPaginator, {static:true})
paginator!:MatPaginator;

@ViewChild(MatSort, {static:true})
sort!: MatSort;




  constructor(private produtoService: ProdutosService, private dialog: MatDialog) { }

  ngOnInit(): void {

    this.produtoService.PegarTodos().subscribe(resultado =>{


      resultado.forEach(produto => {
        this.opcoesProdutos.push(produto.nomedoProduto);

      });

      this.produtos.data= resultado;
      this.produtos.paginator = this.paginator;
      this.produtos.sort = this.sort;
    });

    this.displayedColumns = this.ExibirColunas();
    this.nomeProdutos = this.autocompleteInput.valueChanges.pipe(startWith(''), map(nome => this.FiltrarNomes(nome)));
  
  }


  ExibirColunas():string[]{
    return   ['produtoId','nomedoProduto','valordeVenda','imagem', 'acoes'];
  }

  AbrirDialog(produtoId:number, nomedoProduto:string):void{
      this.dialog.open(DialogExclusaoProdutosComponent, {
        data:{
            produtoId: produtoId,
            nomedoProduto:nomedoProduto
        }
      }).afterClosed().subscribe(resultado=> {
        if(resultado ===true){
          this.produtoService.PegarTodos().subscribe(dados => {
            this.produtos.data = dados;
          });

          this.displayedColumns = this.ExibirColunas();

        }
      });
  }

  FiltrarNomes(nome : string): string[]{

    if(nome.trim().length >= 4){
      this.produtoService
      .FiltrarProdutos(nome.toLowerCase())
      .subscribe((resultado) =>{
          this.produtos.data = resultado;

      });
    }
    else {
      if(nome ===''){
        this.produtoService.PegarTodos().subscribe((resultado) => {
            this.produtos.data = resultado;
        });
      }
    }

    return this.opcoesProdutos.filter(produto=>produto.toLowerCase().includes(nome.toLowerCase()));

  }

}

@Component({
  selector: 'app-dialog-exclusao-produtos',
  templateUrl: 'dialog-exclusao-produtos.html'
})
export class DialogExclusaoProdutosComponent{
  constructor(@Inject( MAT_DIALOG_DATA)public dados:any,
  private produtoService: ProdutosService,
  private snackBar: MatSnackBar ){}

    ExcluirProduto(produtoId: number): void{
      this.produtoService.ExcluirProduto(produtoId).subscribe(resultado =>{
      
        this.snackBar.open(resultado.mensagem, undefined ,{
            duration:2000,
            horizontalPosition: 'right',
            verticalPosition: 'top'
          });
      });
    }

}


