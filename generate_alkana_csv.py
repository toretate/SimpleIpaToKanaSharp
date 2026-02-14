import sys
sys.path.append('temp')
import alkana_data
import random
import csv

# Set seed for reproducibility (same as before)
random.seed(42)

data = alkana_data.data
items = list(data.items())

# Sample 1000 items
sample_size = 1000
if len(items) > sample_size:
    sampled_items = random.sample(items, sample_size)
else:
    sampled_items = items

with open('SimpleIpaToKanaSharp/SimpleIpaToKanaSharp.Tests/alkana_samples.csv', 'w', newline='', encoding='utf-8') as f:
    writer = csv.writer(f)
    writer.writerow(['Word', 'ExpectedKana'])
    for word, kana in sampled_items:
        writer.writerow([word, kana])
