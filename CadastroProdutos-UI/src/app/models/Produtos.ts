import { DadosRegistro } from "./DadosRegistro";

export class Produtos {
    produtoId!:number;
    nomedoProduto!: string; 
    valordeVenda!:number; 
    foto!:File;
    usuarioId!:number; 
    usuario!: DadosRegistro
}