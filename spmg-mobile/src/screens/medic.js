import api from '../services/api'
import React, { Component } from 'react';
import { FlatList, Image, StyleSheet, Text, View } from 'react-native';

export default class Medic extends Component {
    constructor(props) {
        super(props);
        this.state ={
        listaConsulta: [],
        };
    }

    // GET das consultas
    buscarConsultas = async () => {
        const resposta = await api.get('/minhas')
        const dadosApi = resposta.data;
        this.setState({ listaConsulta: dadosApi })
        console.log(dadosApi)
    }

    componentDidMount(){
        this.buscarConsultas();
    }

    render(){
        return(
            <View>
                
            </View>
        )
    }
}

const styles = StyleSheet.create({
  
});