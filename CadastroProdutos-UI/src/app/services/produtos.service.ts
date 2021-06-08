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

    const ApiUrl = `${this.url}/PegarTodos/${usuarioID}`;
    return this.http.get<Produtos[]>(ApiUrl);
 }

  PegarProdutoPeloId(produtoId:number):Observable<Produtos>{
    const apiUrl = `${this.url}/${produtoId}`;
    return this.http.get<Produtos>(apiUrl);
  }

  NovoProduto(produto: Produtos): Observable<any>{
    
    return this.http.post<Produtos>(this.url, produto, httpOptions);
   // return this.http.post<Produtos>(this.url,
  }


AtualizarProdutos(produtoId: number, produto: Produtos): Observable<any>{

  const apiUrl = `${this.url}/${produtoId}`;
  return this.http.put<Produtos>(apiUrl, produto, httpOptions);

}

ExcluirProduto(produtoId: number): Observable<any>{

  const apiUrl = `${this.url}/${produtoId}`;
  return this.http.delete<number>(apiUrl, httpOptions);

}

SalvarFoto(formData: any): Observable<any>{
  const apiUrl = `${this.url}/SalvarFoto`
  console.log(apiUrl);
  return this.http.post<any>(apiUrl, formData);
}

FiltrarProdutos(nomedoProduto: string): Observable<Produtos[]>{

  const ApiUrl = `${this.url}/FiltrarProdutos/${nomedoProduto}`;
  return this.http.get<Produtos[]>(ApiUrl);
}

RetornarFotoProduto(id: string): Observable<any>{
  
  const apiUrl = `${this.url}/RetornarFotoProduto/${id}`;
  return this.http.get<any>(apiUrl);

}

}
