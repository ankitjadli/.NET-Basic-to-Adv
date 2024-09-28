import React from "react";
import { WelcomeAgain , HooksExample} from '../React/reactDemo'
import { useParams } from "react-router-dom";

interface HomeState{
    count : number
}

function GetUserID2() {
    let { userID } = useParams(); // Use useParams to get the userID from the URL
    console.log('UserID in URL: ' + userID);

    return <div>USER ID: {userID}</div>;
}

export class Home extends React.Component<{} , HomeState>{

    constructor(){
        super(''); 
        this.state = { count : 0 };
    }

    getUserID = () => {    
        this.setState({ count : this.state.count + 1})
    }

    render (){
        return <>
            <div>
            </div>
            <GetUserID2/>
            <div className="card">
                <button onClick={this.getUserID}>
                count is {this.state.count}
                </button>
                <p>
                Edit <code>src/App.tsx</code> and save to test HMR
                </p>
            </div>
            <p className="read-the-docs">
                Click on the Vite and React logos to learn more
            </p>
            <div id="JSDemo">
                <div>JsDemo</div>
                <button id="JSDemoButton">JSDemoButton</button>
            </div>
            <WelcomeAgain name='Welcome to the Machine'></WelcomeAgain>
            <HooksExample></HooksExample>
        </>
    }
}