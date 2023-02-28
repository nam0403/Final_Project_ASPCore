import cv2
import numpy as np
import pandas as pd
import os 
from imutils.feature import FeatureDetector_create
from imutils.feature import FeatureDetector_create, DescriptorExtractor_create
import h5py
# List of image filenames
folder_path = 'oxford'
image_ext = ('jpg', 'jpeg', 'png', 'jfif')
img_paths = [os.path.join(folder_path,f) for f in os.listdir(folder_path) if f.endswith(image_ext)]
detector = FeatureDetector_create("BRISK")
descriptor = DescriptorExtractor_create("RootSIFT")
# Create a list to store the descriptors and their corresponding image index
descriptors_list = []
def normalize_vector(vector):
    eps=1e-7
    vector /= (vector.sum(axis=1, keepdims=True) + eps)
    normalized_vector = np.sqrt(vector)
    return normalized_vector
sift = cv2.SIFT_create()
# Loop through the list of images
for i, image in enumerate(img_paths):
    # Load the image
    img = cv2.imread(image)
    img = cv2.resize(img, (320,320))
    gray = cv2.cvtColor(img,cv2.COLOR_BGR2GRAY)
    # Create SIFT object
    kps = detector.detect(gray)
    # Detect keypoints and compute their descriptors
    _, des = descriptor.compute(gray, kps)
    # Convert the descriptors to a numpy array
    descriptors_list.append(des)
# Convert the list to a numpy array
# descriptors_array = np.array(descriptors_list)
with h5py.File('features.h5', 'w') as hf:
    for i, arr in enumerate( descriptors_list):
        # Create a dataset in the file for each array
        hf.create_dataset(f"features_{i}", data=arr, dtype='float32')
print('done writing')
