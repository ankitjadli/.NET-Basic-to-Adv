import { createSlice } from '@reduxjs/toolkit';

export interface stateType{
    count : number
}

const initialState : stateType = {
    count : 0
}

//Here, createSlice generates both the action creators and the reducer for you, making it more concise than traditional Redux.

const counterSlice = createSlice({
    name:'Counter',
    initialState,
    reducers:{
        increment : (state) => {
            state.count += 1
        },
        decrement : (state) =>{
            state.count -= 1
        }
    }
})


export const { increment, decrement} = counterSlice.actions;
export default counterSlice.reducer;