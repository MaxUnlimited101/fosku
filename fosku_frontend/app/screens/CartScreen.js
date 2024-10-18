import React, { useEffect, useState } from 'react';
import { View, Text, FlatList, Button, Alert, StyleSheet } from 'react-native';
import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default function CartScreen() {
  const [cartItems, setCartItems] = useState([]);

  useEffect(() => {
    const fetchCartItems = async () => {
      const token = await AsyncStorage.getItem('token');
      const response = await axios.get('http://your-backend-url/cart', {
        headers: { Authorization: `Bearer ${token}` },
      });
      setCartItems(response.data);
    };
    fetchCartItems();
  }, []);

  const placeOrder = async () => {
    const token = await AsyncStorage.getItem('token');
    await axios.post(
      `http://your-backend-url/orders`,
      {},
      { headers: { Authorization: `Bearer ${token}` } }
    );
    Alert.alert('Success', 'Order placed successfully');
    setCartItems([]);
  };

  return (
    <View style={styles.container}>
      <FlatList
        data={cartItems}
        keyExtractor={(item) => item.productId.toString()}
        renderItem={({ item }) => (
          <View style={styles.cartItem}>
            <Text>{item.productName}</Text>
            <Text>${item.price}</Text>
          </View>
        )}
      />
      {cartItems.length > 0 && <Button title="Place Order" onPress={placeOrder} />}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
  },
  cartItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#ddd',
  },
});
