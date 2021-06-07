import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {HttpClientModule} from '@angular/common/http';
import { ProdutosService } from './services/produtos.service';
import { UsuarioService } from './services/usuario.service';
import { FuncoesService } from './services/funcoes.service';



import { ListagemProdutosComponent, DialogExclusaoProdutosComponent  } from './componentes/Produtos/listagem-produtos/listagem-produtos.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {MatAutocompleteModule}  from '@angular/material/autocomplete';
import {MatTableModule}  from '@angular/material/table';
import {MatIconModule}  from '@angular/material/icon';
import {MatButtonModule}  from '@angular/material/button';
import {MatCardModule}  from '@angular/material/card';
import {MatFormFieldModule}  from '@angular/material/form-field';
import {MatInputModule}  from '@angular/material/input';
import {MatDividerModule} from '@angular/material/divider';
import {MatSelectModule}  from '@angular/material/select';
import {MatGridListModule} from '@angular/material/grid-list';
import{MatDialogModule} from '@angular/material/dialog';
import{MatPaginatorModule} from '@angular/material/paginator';
import{MatSortModule} from '@angular/material/sort';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatToolbarModule} from '@angular/material/toolbar';


import {JwtModule} from '@auth0/angular-jwt';

import { NovoProdutoComponent } from './componentes/Produtos/novo-produto/novo-produto.component';
import { AtualizarprodutoComponent } from './componentes/Produtos/atualizarproduto/atualizarproduto.component';
import { ListagemFuncoesComponent,DialogExclusaoFuncoesComponent } from './componentes/Funcao/listagem-funcoes/listagem-funcoes.component';
import { NovaFuncaoComponent } from './componentes/Funcao/nova-funcao/nova-funcao.component';
import { AtualizarFuncaoComponent } from './componentes/Funcao/atualizar-funcao/atualizar-funcao.component';
import { RegistrarUsuarioComponent } from './componentes/Usuario/Registro/registrar-usuario/registrar-usuario.component';
import { LoginUsuarioComponent } from './componentes/Usuario/Login/login-usuario/login-usuario.component';


import { FlexLayoutModule } from "@angular/flex-layout";
import { DashboardComponent } from './componentes/DashBoard/dashboard/dashboard.component';
import { HeaderComponent } from './componentes/DashBoard/header/header.component';

export function PegarTokenUsuario(){

  return  localStorage.getItem('TokenUsuarioLogado');
       
}

@NgModule({
  declarations: [
    AppComponent,
    ListagemProdutosComponent,
    NovoProdutoComponent,
    AtualizarprodutoComponent,
    DialogExclusaoProdutosComponent,
    ListagemFuncoesComponent,
    NovaFuncaoComponent,
    AtualizarFuncaoComponent,
    DialogExclusaoFuncoesComponent,
    RegistrarUsuarioComponent,
    LoginUsuarioComponent,
    DashboardComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatSelectModule,
    MatGridListModule,
    MatDialogModule,
    FormsModule,
    MatAutocompleteModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    MatProgressBarModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    FlexLayoutModule,
    JwtModule.forRoot({
      config:{
        tokenGetter: PegarTokenUsuario,
        allowedDomains:['http://localhost:5000'],
        disallowedRoutes:[]
      }
    })
  ],
  providers: [
    HttpClientModule,
    ProdutosService,
    UsuarioService,
    FuncoesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
