FROM python:3.8-slim
WORKDIR /app
COPY . /app

RUN pip install flask joblib nltk scikit-learn
RUN python -m nltk.downloader stopwords wordnet

EXPOSE 5000

CMD ["python", "model_controller.py"]