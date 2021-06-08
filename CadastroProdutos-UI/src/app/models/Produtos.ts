import { DadosRegistro } from "./DadosRegistro";

export class Produtos {
    produtoId!:number;
    nomedoProduto!: string; 
    valordeVenda!:number; 
    imagem!:string; 
    foto!:File;
    usuarioId!:number; 
    usuario!: DadosRegistro
}