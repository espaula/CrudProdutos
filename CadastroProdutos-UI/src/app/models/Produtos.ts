import { DadosRegistro } from "./DadosRegistro";

export class Produtos {
    produtoId!:number;
    nomedoProduto!: string; 
    valordeVenda!:number; 
    imagem!:string; 
    usuarioId!:number; 
    usuario!: DadosRegistro
}