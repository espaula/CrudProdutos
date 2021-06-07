import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './componentes/DashBoard/dashboard/dashboard.component';
import { AtualizarFuncaoComponent } from './componentes/Funcao/atualizar-funcao/atualizar-funcao.component';
import { ListagemFuncoesComponent } from './componentes/Funcao/listagem-funcoes/listagem-funcoes.component';
import { NovaFuncaoComponent } from './componentes/Funcao/nova-funcao/nova-funcao.component';

import { AtualizarprodutoComponent } from './componentes/Produtos/atualizarproduto/atualizarproduto.component';
import {ListagemProdutosComponent} from './componentes/Produtos/listagem-produtos/listagem-produtos.component';
import { NovoProdutoComponent } from './componentes/Produtos/novo-produto/novo-produto.component';
import { LoginUsuarioComponent } from './componentes/Usuario/Login/login-usuario/login-usuario.component';
import { RegistrarUsuarioComponent } from './componentes/Usuario/Registro/registrar-usuario/registrar-usuario.component';
import { AuthGuardService } from './services/auth-guard.service';




const routes: Routes = [
  {
    path:'',
    component: DashboardComponent,
    canActivate:[AuthGuardService],
    children:[  {
      path: 'produtos/listagemProdutos', component: ListagemProdutosComponent
    },
    {
      path: 'produtos/novoproduto', component: NovoProdutoComponent
    },
    {
      path: 'produtos/atualizarproduto/:id', component: AtualizarprodutoComponent
    },
    {
      path: 'funcoes/listagemfuncoes', component: ListagemFuncoesComponent
    },
    {
      path: 'funcoes/novaFuncao', component: NovaFuncaoComponent
    },
    {
      path: 'funcoes/atualizarfuncao/:id', component: AtualizarFuncaoComponent
    }
  ]
  },

  {
    path: 'usuario/registrarusuario', component: RegistrarUsuarioComponent
  },
  {
    path: 'usuario/loginusuario', component: LoginUsuarioComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
