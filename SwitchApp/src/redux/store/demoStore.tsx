import { configureStore } from '@reduxjs/toolkit';
import counterReducer  from '../reducers/demoReducer'

const store = configureStore({
    reducer : {
        counter : counterReducer
    }
});

export default store;