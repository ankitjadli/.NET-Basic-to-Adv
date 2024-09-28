import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom'; // Optional but useful for better assertions
import { HooksExample } from '../screens/dashboard/React/reactDemo';

test('it should increment the counter when the button is clicked', () => {
  // Render the Counter component
  render(<HooksExample />);

  // Find the initial elements
  const counterText = screen.getByText(/State Value :/i);
  const incrementButton = screen.getByText(/Hooks Example/i);
  //screen.getByTestId
    
  // Assert initial value
  expect(counterText).toHaveTextContent('State Value :');

  // Simulate a button click
  fireEvent.click(incrementButton);

  // Assert the updated value
  expect(counterText).toHaveTextContent('State Value : Audi');
});
