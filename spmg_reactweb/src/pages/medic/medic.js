import { Component } from 'react';
import "../../assets/css/medic.css"
import "../../assets/css/adm.css"

import patient from "../../assets/img/paciente.svg"
import calendar from "../../assets/img/calendar.svg"
import description from "../../assets/img/medical-record.svg"

import Rodape from "../../assets/components/Rodape/rodape"
import Header from "../../assets/components/Header/header"
import axios from 'axios';

class Medic extends Component{
    constructor(props){
        super(props)
        this.state = {
            listaConsulta : [],
            listaPaciente : [],
            listaSituacao: [],
            IdConsultaAlterada: 0,
            IdMedico : '',
            IdPaciente : '',
            IdSituação : '',
            Data : '',
            Desc : ''
        }
    }

    editarDesc = (event) => {
        event.preventDefault();

        if (this.state.IdConsultaAlterada !== 0) {
            //Edição

            fetch('http://localhost:5000/api/Consulta/' + this.state.IdConsultaAlterada, {
                method : 'PUT',

                body : JSON.stringify({ descricao : this.state.Desc }),

                headers : {
                    'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
                    "Content-Type" : "application/json"
                }
            })

            .then(resposta => {
                if (resposta.status === 204) {
                    console.log('Consulta ' + this.state.IdConsultaAlterada + ' atualizado!',
                    'Sua nova descrição agora é: ' + this.state.Desc)
                }
            })

            .then(this.buscarConsultas)
    }
    }

    buscarConsultas = () => {
        fetch('http://localhost:5000/api/consulta/medico',{
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
                "Content-Type" : "application/json"
            }
        })

        .then(resposta => resposta.json())

        .then(dados => this.setState({ listaConsulta : dados }))

        .catch(erro => console.log(erro))
    }

    buscarConsultaPorId = (Consulta) => {
        this.setState({
            IdConsultaAlterada : Consulta.idConsulta,
            Desc : Consulta.descricao
        }, () => {
            console.log(
                'A descrição ' + Consulta.idConsulta + ' foi selecionada, ',
                'agora o valor do state IdConsultaAlterada é: ' + this.state.IdConsultaAlterada, 
                'e o valor do state Desc é: ' + this.state.descricao
            )
        })
    }

    atualizaEstadoDesc = async (event) => {
        await this.setState({ Desc : event.target.value })
        console.log(this.state.Desc)
    }

    componentDidMount(){
        this.buscarConsultas()

    }

    render(){
        return(
            <div className="body-medic">
                <Header />
                    <div className="body-content">
                        <section className="cadastro-usuario-adm">
                            <div className="cadastro-content-adm">
                                <form onSubmit={this.editarDesc}>
                                    <div>
                                        <input type="text" value={this.state.Desc} onChange={this.atualizaEstadoDesc} placeholder="Insira a descrição" />
                                        <button type="submit">Editar Descrição</button>
                                    </div>
                                </form>
                            </div>
                        </section>
                        {
                            this.state.listaConsulta.map( Consulta => {
                                return(
                                    
                                    <section className="consulta">
                                        <div className="left">
                                            <div className="consulta-content">
                                                <div className="title">
                                                    <h1>{Consulta.idSituacaoNavigation.situacao1}</h1>
                                                    <h2>Consulta no {Consulta.idMedicoNavigation.idEspecialidadeNavigation.especialidade1}</h2>
                                                </div>
                                                <div className="data flex-center">
                                                        <img src={calendar} alt="" />
                                                        <div className="column">
                                                            <p>Data</p>
                                                            <p>{Consulta.dataConsulta}</p>
                                                        </div>
                                                </div>
                                                <div className="medic flex-center">
                                                    <img src={patient} alt="" />
                                                    <div className="column">
                                                        <p>Paciente:</p>
                                                        <p>{Consulta.idPacienteNavigation.nomePaciente}</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="right">
                                            <div className="icon">
                                                <img src={description} alt="" />
                                                <h1>Descrição</h1>
                                            </div>
                                            <div className="text">
                                                <div className="description-content">
                                                    <p>{Consulta.descricao}</p>
                                                    <button onClick={() => this.buscarConsultaPorId(Consulta)}>Selecionar Descrição</button>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                )
                            })
                        }
                    </div>
                <Rodape />
            </div>
        )
    }
}

export default Medic;