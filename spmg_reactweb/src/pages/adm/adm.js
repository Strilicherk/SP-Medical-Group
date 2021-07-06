import { Component } from 'react';
import "../../assets/css/adm.css"

import calendar from "../../assets/img/calendar.svg"
import medic from "../../assets/img/medic.svg"
import patient from "../../assets/img/paciente.svg"
import Header from "../../assets/components/Header/header"
import axios from 'axios';

class Adm extends Component{
    constructor(props){
        super(props)
        this.state = {
            listaConsulta : [],
            IdConsultaAlterada: 0,
            listaMedico: [],
            listaPaciente: [],
            listaSituacao: [],
            isLoading: false,
            IdMedico : '',
            IdPaciente : '',
            IdSituacao : '',
            Data : '',
            Desc : '',
        }
    }

    buscarConsultas = () => {

        fetch('http://localhost:5000/api/consulta',{
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
                "Content-Type" : "application/json"
            }
        })

        .then(resposta => resposta.json())

        .then(dados => this.setState({ listaConsulta : dados }))

        .catch(erro => console.log(erro))
    }

    buscarMedico = () =>{
        axios('http://localhost:5000/api/Medico', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
            }
        })
        .then(resposta => {
            if(resposta.status === 200){
                this.setState({ listaMedico : resposta.data})
                console.log(this.state.listaMedico)
            }
        })
        .catch(erro => console.log(erro))
    }

    buscarPaciente = () => {
        axios('http://localhost:5000/api/Paciente', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
            }
        })
        .then(resposta => {
            if(resposta.status === 200){
                this.setState({ listaPaciente : resposta.data})
                console.log(this.state.listaPaciente)
            }
        })
        .catch(erro => console.log(erro))
    }

    buscarSituacao = () =>{
        axios('http://localhost:5000/api/situacao', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
            }
        })
        .then(resposta => {
            if(resposta.status === 200){
                this.setState({ listaSituacao : resposta.data})
                console.log(this.state.listaSituacao)
            }
        })
        .catch(erro => console.log(erro))
    }

    atualizaEstadoMedico = async (event) => {
        await this.setState({ IdMedico : event.target.value })
        console.log(this.state.IdMedico)
    }

    atualizaEstadoPaciente = async (event) => {
        await this.setState({ IdPaciente : event.target.value })
        console.log(this.state.IdPaciente)
    }

    atualizaEstadoSituacao = async (event) => {
        await this.setState({ IdSituacao : event.target.value })
        console.log(this.state.IdSituacao)
    }

    atualizaEstadoData = async (event) => {
        await this.setState({ Data : event.target.value })
        console.log(this.state.Data)
    }

    cadastrarConsulta = (event) => {
        event.preventDefault();
            fetch('http://localhost:5000/api/consulta', {
                method : 'POST',
    
                body: JSON.stringify({ idMedico : this.state.IdMedico, idPaciente : this.state.IdPaciente, idSituacao : this.state.IdSituacao, dataConsulta : this.state.Data }),
    
                headers : {
                    'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
                    "Content-Type" : "application/json"
                }   
            })
    
            .then(console.log('Consulta registrada'))
    
            .catch(erro => console.log(erro))
    
            .then(this.buscarConsultas)
    }

    editarSituacao = (event) => {
        if (this.state.IdConsultaAlterada !== 0) {
            //Edição

            fetch('http://localhost:5000/api/Consulta/' + this.state.IdConsultaAlterada, {
                method : 'PUT',

                body : JSON.stringify({ idSituacao : this.state.IdSituacao }),

                headers : {
                    "Content-Type" : "application/json",
                    'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
                }
            })

            .then(resposta => {
                if (resposta.status === 204) {
                    console.log('Consulta ' + this.state.IdConsultaAlterada + ' atualizado!',
                    'Sua nova situação agora é: ' + this.state.IdSituacao)
                }
            })

            .then(this.buscarConsultas)

        } 
    }

    componentDidMount(){
        this.buscarConsultas()
        this.buscarMedico()
        this.buscarPaciente()
        this.buscarSituacao()
    }

    buscarConsultaPorId = (Consulta) => {
        this.setState({
            IdConsultaAlterada : Consulta.idConsulta,
            IdSituacao : Consulta.idSituacao
        }, () => {
            console.log(
                'A consulta ' + Consulta.idConsulta + ' foi selecionada, ',
                'agora o valor do state IdConsultaAlterada é: ' + this.state.IdConsultaAlterada, 
                'e o valor do state idsituacao é: ' + this.state.IdSituacao
            )
        })
    }


    render(){
        return(
            <div>
                <Header />
                <main>
                    <body className="body-content-adm">
                        <section className="cadastro-usuario-adm">
                            <div className="cadastro-content-adm">
                                <h1>Insira as informações da consulta que deseja cadastrar:</h1>
                                <form onSubmit={this.cadastrarConsulta}>
                                    <div>
                                        <input type="number" value={this.state.IdMedico} onChange={this.atualizaEstadoMedico} placeholder="Id do Médico"/>
                                        <input type="number" value={this.state.IdPaciente} onChange={this.atualizaEstadoPaciente} placeholder="Id do Paciente"/>
                                        <input type="number" value={this.state.IdSituação} onChange={this.atualizaEstadoSituacao} placeholder="Id da Situação"/>
                                        <input type="date" value={this.state.Data} onChange={this.atualizaEstadoData} placeholder="Data"/>
                                        <button type="submit">Cadastrar</button>
                                    </div>
                                </form>
                            </div>
                        </section>
                        <section className="list-adm">
                                    {
                                        this.state.listaConsulta.map( Consulta => {
                                            return(
                                                <div className="list-content-adm">
                                                    <div className="consulta-adm">
                                                        <div className="infos-adm">
                                                            <div className="title-adm">
                                                                <h1>Consulta no {Consulta.idMedicoNavigation.idEspecialidadeNavigation.especialidade1}</h1>
                                                                <h2>
                                                                    {Consulta.idSituacaoNavigation.situacao1}
                                                                    <button onClick={() => this.buscarConsultaPorId(Consulta)}>Selecionar Situação</button>
                                                                    <form onSubmit={this.editarSituacao}>
                                                                        <div>
                                                                            <input type="text" value={this.state.IdSituacao} onChange={this.atualizaEstadoSituacao} placeholder="Insira a situação" />
                                                                            <button type="submit">Editar Situação</button>
                                                                        </div>
                                                                    </form>
                                                                </h2>
                                                            </div>
                                                            <div className="medic-adm">
                                                                <img src={medic} alt="" />
                                                                <div className="medic-info-adm">
                                                                <h1>Médico:</h1>
                                                                <p>{Consulta.idMedicoNavigation.nomeMedico}</p>
                                                                </div>
                                                            </div>
                                                            <div className="paciente-adm">
                                                                <img src={patient} alt="" />
                                                                <div className="patient-info-adm">
                                                                <h1>Paciente:</h1>
                                                                <p>{Consulta.idPacienteNavigation.nomePaciente}</p>
                                                                </div>
                                                            </div>
                                                            <div className="paciente-adm">
                                                                <img src={calendar} alt="" />
                                                                <div className="patient-info-adm">
                                                                <h1>Data:</h1>
                                                                <p>{Consulta.dataConsulta}</p>
                                                                </div>
                                                            </div>
                                                            <div className="desc-adm">
                                                                <h1>Descrição</h1>
                                                                <div className="text-adm">
                                                                    <div className="description-adm">
                                                                        <p>{Consulta.descricao}</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            )
                                        })
                                    }
                        </section>
                    </body>               
                </main>
            </div>
        )
    }

}

export default Adm;