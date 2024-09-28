import React, { Suspense, useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react';
import { stateType , increment, decrement} from '../../../redux/reducers/demoReducer'
import { useDispatch, useSelector } from 'react-redux';

// React is a JS libarary used for building UI
// React is component based. UI is broken down into small, isolated components.

// JSX -> Javascript XML allow dev to write HTML directly in JS code.

const e = <div> JSX Example </div>;


// Function Components

// Define an interface for the state
interface WelcomeAgainState {
    CarName: string;
    Count: number;
}

function Welcome(props : any){
    return <div> {props.name} </div>;
}

// Class Components
// Before hooks, class were the way to handle state.

export class WelcomeAgain extends React.Component< { name : string}, WelcomeAgainState> { // The first type ({}) means there are no specific props for this component.

    constructor(props: any) {
        super(props);
        this.state = { CarName: '', Count: 0 }; // Initialize state
    }

    setCarName = (carName: string) => {
        this.setState({ CarName: carName, Count: this.state.Count + 1 }); // Update state
    };

    List = (params : number[]) => {
        let c = params.map(x => {
            return <div>{x}</div>;
        })

        return c;
    }

    componentDidMount(): void {
        // Triggers when component output of the component is rendered in the DOM.    
    }

    shouldComponentUpdate(nextProps: Readonly<{ name: string; }>, nextState: Readonly<WelcomeAgainState>, nextContext: any): boolean {
        
        // optmize rendering by determinig wherther the componet needs to be re-rendered or not.

        return true;
    }

    componentWillUnmount(): void {
        //When component is removed from DOM.
    }

    Keys = (params : number[]) => {
        
        let d = params.map(x => {
            return <div key={x}>{x}</div>; // help react to identify which item in the list is changed, optimizes the rendering
        });

        return d;
    }

    // Example of conditional rendering and use of state.
    render (){
        return <>
            <div>{this.props.name}</div>
            {this.state.CarName == 'Audi' ? <div>Car Name {this.state.CarName}</div> : <></>} 
            <button onClick={() => this.setCarName('Audi')}>Click ME!!</button>
            {
                this.List([1,3,4])
            }
            {
                this.Keys([4,5,6])
            }
        </>
    }
}

function useCustomHook() {
    const [value, setValue] = useState(0); // Initialize the state
    return [value, setValue]; // Return an array with value and setter function
}

function childComponent() {

    //ContextAPI provide a way to pass data through the component tree without passing porps
    const themeContext = React.createContext('light2');

    return <div>{useContext(themeContext)}</div>;
}

// React hooks can only be used inside functional components, not class components
export function HooksExample(){

    const [car, setCar] = useState('');
    const [val, setVal] = useCustomHook(); 
    const ref = useRef(null); //useRef can be used to directly reference a DOM element created by a React component
    
    function setCarMethod (carName : string) {
        setCar(carName);
    }

    useEffect(() => {
        // Trigger when Value of Car Cahanges
    },[car]);

    useEffect(() => {
        // Trigger when Component renders
        console.log('UseEffect called for HooksExample component');
    });

    //Performance Optimization
    //React Memo : High order component memorizes the result until props are changed.sssssssssssssssssssssssssssss
    const Momorized = React.memo(childComponent);

    // useCallback : functions are only recreated when their dependencies change
    const callBackExample = useCallback(() => {
        setCar(car);
    }, [car, val]); // Only re-create the function if car or val changes

    // We can use useMemo to cache the result and only recalculate it when the input number changes
    const memoizedValue = useMemo(() => {
        // Expensive calculation or operation
        return car;
    }, [car]); // Dependencies array
    
    // Lazy loading
    const LazyLoadedHome = React.lazy(() => import('../../../App'));
    //Suspense is used to show loading page till import is finished

    //Redux
    const dispatch = useDispatch();

    // Get value from redux
    const countcurrent = useSelector((state : any) => state.counter.count);

    function IncrementButtonClick(){
        dispatch(increment());
    }

    function DeccrementButtonClick(){
        dispatch(decrement());
    }
    
    return <>
        <div>State Value : {car}</div>
        <button onClick={()=> setCarMethod("Audi")}>Hooks Example</button>
        <button onClick={IncrementButtonClick}>Increment Redux</button>
        <button onClick={DeccrementButtonClick}>Decrement Redux</button>
        <div>Redux Working : {countcurrent}</div>
        <Momorized/>
        <Suspense fallback={<div>Loading</div>}>
            <LazyLoadedHome/>
        </Suspense>
    </>
    
}
