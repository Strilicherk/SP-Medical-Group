import axios from 'axios';
import { Component } from 'react';
import "../../assets/css/patient.css"

import calendar from "../../assets/img/calendar.svg"
import medic from "../../assets/img/medic.svg"
import medicalrecord from "../../assets/img/medical-record.svg"

import Rodape from "../../assets/components/Rodape/rodape"
import Header from "../../assets/components/Header/header"


class Patient extends Component{
    constructor(props){
        super(props)
        this.state = {
            listaConsulta : [],
        }
    }

    buscarConsulta = () =>{
        axios('http://localhost:5000/api/consulta/paciente', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login'),
                
            }
        })
        .then(resposta => {
            if(resposta.status === 200){
                this.setState({ listaConsulta : resposta.data})
                console.log(this.state.listaConsulta)
            }
        })
        .catch(erro => console.log(erro))
    }

    componentDidMount(){
        this.buscarConsulta()
    }

    render(){
        return(
            <div className="body-patient">
                <Header />
                    <div className="body-content">
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
                                                    <img src={medic} alt="" />
                                                    <div className="column">
                                                        <p>Médico:</p>
                                                        <p>{Consulta.idMedicoNavigation.nomeMedico}</p>
                                                        <p>CRM: {Consulta.idMedicoNavigation.crm}</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="right">
                                            <div className="icon">
                                            <img src={medicalrecord} alt="" />
                                            <h1>Descrição</h1>
                                            </div>
                                            <div className="text">
                                                <div className="description-content">
                                                    <p>{Consulta.descricao}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                )
                            })
                        }
                    </div>
                    {/* <table>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Medico</th>
                                <th>Situação</th>
                                <th>Data</th>
                                <th>Descrição</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.listaConsulta.map( Consulta =>{
                                    return(
                                        <tr key={Consulta.idConsulta}>
                                            <td>{Consulta.idConsulta}</td>
                                            <td>{Consulta.idMedicoNavigation.nomeMedico}</td>
                                            <td>{Consulta.idSituacaoNavigation.situacao1}</td>
                                            <td>{Consulta.dataConsulta}</td>
                                            <td>{Consulta.descricao}</td>
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table> */}
                <Rodape />
            </div>
        )
    }
}

export default Patient