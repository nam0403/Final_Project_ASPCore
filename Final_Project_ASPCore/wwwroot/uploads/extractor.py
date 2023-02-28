import h5py
import numpy as np
import os
from skimage.feature import SIFT
import cv2 as cv 
import csv
from sklearn.decomposition import PCA


folder_path = 'oxford'
image_ext = ('jpg', 'jpeg', 'png', 'jfif')
img_paths = [os.path.join(folder_path,f) for f in os.listdir(folder_path) if f.endswith(image_ext)]

fea_extractor = cv.SIFT_create()
features, image_paths = [], []
pca = PCA(n_components=32)
def extract_feature(img_paths):
    for img_path in img_paths:
        try:
            img = cv.imread(img_path)
            # get features from SIFT descriptor extractor
            gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
            _, des = fea_extractor.detectAndCompute(gray, None)
            # print(des.shape[0])
            # features.append(des)
            pca.fit(des)
            data_pca = pca.transform(des)
            features.append(data_pca)
            image_paths.append(img_path)
            # print(features[0])
        except:
            pass
extract_feature(img_paths)
print('done extracting')
with open('image_paths.csv', 'w', newline='') as csvfile:
    writer = csv.writer(csvfile)
    # Write header row
    writer.writerow(['Image Path'])
    # Write image path
    for img_path in image_paths:
        writer.writerow([img_path])
# print(features)
# features = np.concatenate(features, axis=0)
with h5py.File('features_rootsift.h5', 'w') as hf:
    for i, arr in enumerate(features):
        # Create a dataset in the file for each array
        hf.create_dataset(f"features_{i}", data=arr, dtype='float32')
print('done writing')

