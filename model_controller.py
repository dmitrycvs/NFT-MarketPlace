from flask import Flask, request, jsonify
import joblib
from main import clean_text

app = Flask(__name__)

with open('moderation_model.pkl', 'rb') as file:
    model = joblib.load(file)

with open('vectorizer.pkl', 'rb') as file:
    vectorizer = joblib.load(file)

@app.route('/predict', methods=['POST'])
def predict():
    data = request.get_json()
    texts_data = data.get('texts', [])
    if not texts_data:
        return jsonify({'error': 'No texts provided'}), 400
    
    texts_cleaned = [clean_text(text) for text in texts_data]
    texts_vectorized = vectorizer.transform(texts_cleaned)

    prediction = model.predict(texts_vectorized)
    
    return jsonify({'Prediction made': prediction.tolist()})

app.run(host="0.0.0.0", port=5000, debug=True)