import React, { Component } from "react";
import axios from 'axios';
import { parseJwt, usuarioAutenticado } from '../../services/auth'

import "../../assets/css/login.css";

import medica from "../../assets/img/medica.png"
import logo from "../../assets/img/logo.svg"

class Login extends Component{
    constructor(props){
        super(props);
        this.state = {
          email : '',
          senha : '',
          erroMensagem : '',
          isLoading : false
        }
    }

    efetuaLogin = (event) => {
      event.preventDefault()

      this.setState({ erroMensagem : '', isLoading : true })

      axios.post('http://localhost:5000/api/login', {
        email : this.state.email,
        senha : this.state.senha
      })

      .then(resposta => {
        if (resposta.status === 200){
            localStorage.setItem('usuario-login', resposta.data.token)

            this.setState({ isLoading : false})

            console.log(parseJwt())
            console.log(parseJwt().role)

            if (parseJwt().role ==="1") {
              console.log(usuarioAutenticado())
              this.props.history.push('/adm/consultas')
            } else if (parseJwt().role ==="2") {
              console.log(usuarioAutenticado())
              this.props.history.push('/medic/consultas')
            } else if (parseJwt().role ==="3") {
              console.log(usuarioAutenticado())
              this.props.history.push('/patient/consultas')
            }
        }
      })

      .catch(() =>{
        this.setState({ erroMensagem: 'E-mail ou senha inválidos! Tente novamente.', isLoading : false })
      })
    }
    
    atualizaStateCampo = async (campo) => {
      await this.setState({ [campo.target.name] : campo.target.value})  
    }

    render(){
      return (
            <div className="body-login">
              <div className="left-login">
                <img src={medica} alt="Fotografia de uma enfermeira utilizando máscara." className="left-img-login" />
                <div className="cor-login" />
              </div>
              <section className="right-login">
                <div className="div-logo-login">
                  <img src={logo} alt="logo SP Medical Group." className="logo-login" />
                </div>
                <div className="cor2-login" />
                <div className="div-forms-login">
                  <form onSubmit={this.efetuaLogin}>
                    <input type="text" name="email" value={this.state.email} onChange={this.atualizaStateCampo} placeholder="Insira seu e-mail:" className="input-login" />
                    <input type="text" name="senha" value={this.state.senha} onChange={this.atualizaStateCampo} placeholder="Insira sua senha:" className="input-login" />

                    <p style={{color : 'red'}}>{this.state.erroMensagem}</p>
                      
                    {this.state.isLoading === false && 
                    <button type="submit" className="login-login" disabled={this.state.email===''|| this.state.senha === '' ?'none': ''}>LOGIN</button>}
                    {this.state.isLoading === true && <button type="submit" disabled>Loading...</button>}
                  </form>
                </div>
                <div className="div-login-login">
                  <a rel="stylesheet" href="#" className="a-login">Esqueceu a sua senha? Clique aqui</a>
                </div>
              </section>
            </div>
          );
    }
}

export default Login;
