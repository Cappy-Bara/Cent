function getDetunedUpperSoundFrequency(frequency, cents) {
  var nextTone = getNextFrequency(frequency);
  var freqDiff = nextTone - frequency;
  var centileDiff = freqDiff / 100;
  var newFreq = frequency + (centileDiff * cents);
  return newFreq;
}

function getNextFrequency(currentFrequency) {
  const val = 1 / 12;
  return currentFrequency * Math.pow(2, val);
}

function playSound(freq, len) {
  const osc = new Tone.Oscillator(freq, "sine").toDestination().start();
  setTimeout(() => { osc.stop(); osc.dispose(); }, len);
}