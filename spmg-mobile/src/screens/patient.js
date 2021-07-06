import api from '../services/api'
import React, { Component } from 'react';
import { FlatList, Image, StyleSheet, Text, View } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default class Patient extends Component {
    constructor(props) {
        super(props);
        this.state ={
        listaConsulta: [],
        };
    }

    // GET das consultas
    buscarConsultas = async () => {
        const valorToken = await AsyncStorage.getItem('userToken')

        const resposta = await api.get('consulta/paciente', {
            headers: {
                'Authorization' : 'Bearer ' + valorToken
            }
        })

        const dadosApi = resposta.data;
        this.setState({ listaConsulta: dadosApi })
        console.log(dadosApi)
    }

    componentDidMount(){
        this.buscarConsultas();
    }

    render(){
        return(
            <View style={styles.main}>
                <View style={styles.mainBody}>
                    <FlatList 
                        contentContainerStyle={styles.mainBodyContent}
                        data={this.state.listaConsulta}
                        keyExtractor={(item) => item.nome}
                        renderItem={this.renderizaItem}
                    />
                </View>
            </View>
        )
    }

    renderizaItem= ({item}) => (
        <View style={styles.flatItemRow}>
            <View style={styles.flatItemContainer}>
                <Text style={styles.flatItemTitle}>Consulta no {item.idMedicoNavigation.idEspecialidadeNavigation.especialidade1}</Text>
                <Text style={styles.flatItemTitle}>{item.idSituacaoNavigation.situacao1}</Text>
                <View style={styles.flatItem}>
                    <Image
                        source={require('../../assets/img/calendar.svg')}
                        style={styles.flatItemImgIcon}
                    />
                    <Text style={styles.flatItemInfo}>Data da Consulta: {" "} {Intl.DateTimeFormat("pt-BR").format(new Date(item.dataConsulta))}</Text>
                </View>
                <View style={styles.flatItem}>
                    <Image
                        source={require('../../assets/img/medic.svg')}
                        style={styles.flatItemImgIcon}
                    />
                    <Text style={styles.flatItemInfo}>Medico: {item.idMedicoNavigation.nomeMedico}</Text>
                </View>
                <View style={styles.flatItem}>
                    <Image
                        source={require('../../assets/img/medical-record.svg')}
                        style={styles.flatItemImgIcon}
                    />
                    <Text style={styles.flatItemInfo}>Diagnóstico: {item.descricao}</Text>
                </View>
            </View>
        </View>
    )
    
}

const styles = StyleSheet.create({
    main: {
        flex: 1,
        backgroundColor: '#F1F1F1'
    },

    mainBody: {
        flex: 4,
        backgroundColor: '#2692D0',
    },

    mainBodyContent: {
        paddingTop: 30,
        paddingRight: 50,
        paddingLeft: 50
    },

    flatItemRow: {
        flexDirection: 'row',
        borderBottomWidth: 1,
        borderBottomColor: '#ccc',
        marginTop: 30,
    },

    flatItemContainer: {
        flex: 1,
    },

    flatItemTitle: {
        fontSize: 28,
        color: 'black',
        fontFamily: 'Open Sans Light'
    },

    flatItem: {
        flexDirection: 'row',
    },

    flatItemInfo: {
        fontSize: 20,
        color: 'black',
        lineHeight: 20,
    },

    flatItemImgIcon: {
        width: 26,
        height: 26,
        margin: 10,
    }
});