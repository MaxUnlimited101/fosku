import React, { useEffect, useState } from 'react';
import { View, Text, Button, StyleSheet, Alert } from 'react-native';
import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default function ProductScreen({ route }) {
  const { productId } = route.params;
  const [product, setProduct] = useState(null);

  useEffect(() => {
    const fetchProduct = async () => {
      const token = await AsyncStorage.getItem('token');
      const response = await axios.get(`http://your-backend-url/products/${productId}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      setProduct(response.data);
    };
    fetchProduct();
  }, [productId]);

  const addToCart = async () => {
    const token = await AsyncStorage.getItem('token');
    await axios.post(
      `http://your-backend-url/cart`,
      { productId },
      { headers: { Authorization: `Bearer ${token}` } }
    );
    Alert.alert('Success', 'Product added to cart');
  };

  if (!product) {
    return <Text>Loading...</Text>;
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>{product.name}</Text>
      <Text>{product.description}</Text>
      <Text style={styles.price}>${product.price}</Text>
      <Button title="Add to Cart" onPress={addToCart} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  price: {
    fontSize: 20,
    color: 'green',
    marginVertical: 20,
  },
});
