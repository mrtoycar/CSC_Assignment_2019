import React from 'react';
import { StyleSheet, css } from 'aphrodite'

const Clarifai = require("clarifai");

// import ClarifaiApp from 'clarifai.rest';

class App extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            image: undefined,
            concepts: []
        }
    }

    componentDidMount() {
        // const app = new Clarifai.App({
        //   apiKey: "143a9e7649f64f38a94e7192d7170e9c"
        // });
        // app.models.initModel({id: Clarifai.GENERAL_MODEL, version: "aa7f35c01e0642fda5cf400f543e7c40"})
        //   .then(generalModel => {
        //     // console.log(generalModel);
        //     return generalModel.predict("https://samples.clarifai.com/metro-north.jpg");
        //   })
        //   .then(response => {
        //     var concepts = response['outputs'][0]['data']['concepts']
        //     console.log()
        //     // console.log(response);
        //     // console.log(concepts)
        //   })
    }

    onImageChange = (e) => {
        const app = new Clarifai.App({
            apiKey: "143a9e7649f64f38a94e7192d7170e9c"
        });
        const reader = new FileReader();
        reader.onloadend = () => {
            this.setState({
                image: reader.result
            })
            let base64String = reader.result.split(',').pop();

            app.models.predict(Clarifai.GENERAL_MODEL, { base64: base64String })
                .then((res) => {
                    var concepts = res['outputs'][0]['data']['concepts']
                    this.setState({
                        concepts
                    })
                    console.log(concepts);
                }, (err) => {
                    console.log(err);
                })
        }
        const image = e.target.files[0];
        reader.readAsDataURL(image);
    }


    render() {
        const { concepts } = this.state;
        return (
            <div className={css(styles.bodyContainer)}>
                <img className={css(styles.image)} src={this.state.image} />
                <p>Input an image</p>
                <input type="file" name="image" onChange={this.onImageChange} />
                <ul>
                    <h3>Features</h3>
                    {concepts.map((concept, index) => {
                        return (
                            <li className={css(styles.listItem)} key={index}>
                                Name: {concept.name} <br />
                                Accuracy: {concept.value}
                            </li>
                        )
                    })}
                </ul>
            </div>
        );
    }
}

const styles = StyleSheet.create({
    bodyContainer: {
        margin: '15px'
    },
    image: {
        width: '100%',
        maxHeight: '450px',
        objectFit: 'cover'
    },
    listItem: {
        marginBottom: '10px',
        border: '1px solid black',
        padding: '5px',
        listStyleType: 'none'
    }
})

export default App;
