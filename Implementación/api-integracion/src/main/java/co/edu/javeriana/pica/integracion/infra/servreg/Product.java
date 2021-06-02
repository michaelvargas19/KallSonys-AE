package co.edu.javeriana.pica.integracion.infra.servreg;

public class Product {
    private String code;
    private String SKU;
    private String name;
    private String availability;
    private String brand_id;
    private String brand_name;
    private String condition;
    private String description;
    private String order_quantity_maximum;
    private String order_quantity_minimum;
    private Double price;
    private Double sale_price;
    private String type;
    private Boolean is_free_shipping;
    private String idProveedor;

    public Product(){
        super();
    }

    public Product(String code, String SKU, String name, String availability, String brand_id, String brand_name, String condition, String description, String order_quantity_maximum, String order_quantity_minimum, Double price, Double sale_price, String type, Boolean is_free_shipping, String idProveedor) {
        this();
        this.code = code;
        this.SKU = SKU;
        this.name = name;
        this.availability = availability;
        this.brand_id = brand_id;
        this.brand_name = brand_name;
        this.condition = condition;
        this.description = description;
        this.order_quantity_maximum = order_quantity_maximum;
        this.order_quantity_minimum = order_quantity_minimum;
        this.price = price;
        this.sale_price = sale_price;
        this.type = type;
        this.is_free_shipping = is_free_shipping;
        this.idProveedor = idProveedor;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public String getSKU() {
        return SKU;
    }

    public void setSKU(String SKU) {
        this.SKU = SKU;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAvailability() {
        return availability;
    }

    public void setAvailability(String availability) {
        this.availability = availability;
    }

    public String getBrand_id() {
        return brand_id;
    }

    public void setBrand_id(String brand_id) {
        this.brand_id = brand_id;
    }

    public String getBrand_name() {
        return brand_name;
    }

    public void setBrand_name(String brand_name) {
        this.brand_name = brand_name;
    }

    public String getCondition() {
        return condition;
    }

    public void setCondition(String condition) {
        this.condition = condition;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getOrder_quantity_maximum() {
        return order_quantity_maximum;
    }

    public void setOrder_quantity_maximum(String order_quantity_maximum) {
        this.order_quantity_maximum = order_quantity_maximum;
    }

    public String getOrder_quantity_minimum() {
        return order_quantity_minimum;
    }

    public void setOrder_quantity_minimum(String order_quantity_minimum) {
        this.order_quantity_minimum = order_quantity_minimum;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public Double getSale_price() {
        return sale_price;
    }

    public void setSale_price(Double sale_price) {
        this.sale_price = sale_price;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Boolean getIs_free_shipping() {
        return is_free_shipping;
    }

    public void setIs_free_shipping(Boolean is_free_shipping) {
        this.is_free_shipping = is_free_shipping;
    }

    public String getIdProveedor() {
        return idProveedor;
    }

    public void setIdProveedor(String idProveedor) {
        this.idProveedor = idProveedor;
    }
}
