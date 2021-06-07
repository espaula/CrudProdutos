import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produtos } from '../models/Produtos';


const httpOptions = {
  headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('TokenUsuarioLogado')}`
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProdutosService {

  url = 'api/Produtos'
  constructor(private http:HttpClient) { }

  PegarTodos(): Observable<Produtos[]>{
    
    var usuarioID = localStorage.getItem('UsuarioId');
console.log(usuarioID);

    const ApiUrl = `${this.url}/PegarTodos/${usuarioID}`;
    console.log(ApiUrl);
    return this.http.get<Produtos[]>(ApiUrl);
 }

  PegarProdutoPeloId(produtoId:number):Observable<Produtos>{
    const apiUrl = `${this.url}/${produtoId}`;
    return this.http.get<Produtos>(apiUrl);
  }

  NovoProduto(produto: Produtos): Observable<any>{
    

    console.log(produto);
    return this.http.post<Produtos>(this.url, produto, httpOptions);
   // return this.http.post<Produtos>(this.url,
  }


AtualizarProdutos(produtoId: number, produto: Produtos): Observable<any>{

  console.log("teste");
  const apiUrl = `${this.url}/${produtoId}`;
  return this.http.put<Produtos>(apiUrl, produto, httpOptions);

}

ExcluirProduto(produtoId: number): Observable<any>{

  const apiUrl = `${this.url}/${produtoId}`;
  return this.http.delete<number>(apiUrl, httpOptions);

}

FiltrarProdutos(nomedoProduto: string): Observable<Produtos[]>{

  const ApiUrl = `${this.url}/FiltrarProdutos/${nomedoProduto}`;
  console.log(ApiUrl);
  return this.http.get<Produtos[]>(ApiUrl);
}

}