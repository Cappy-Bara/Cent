function getDetunedUpperSoundFrequency(frequency, cents) {
  return frequency * Math.pow(2, cents / 1200);
}

function getNextFrequency(currentFrequency) {
  const val = 1 / 12;
  return currentFrequency * Math.pow(2, val);
}

function playSound(freq, len) {
  const osc = new Tone.Oscillator(freq, "sine").toDestination().start();
  setTimeout(() => { osc.stop(); osc.dispose(); }, len);
}