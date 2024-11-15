PGDMP                  
    |            MHW-API    16.4    16.4 S    W           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            X           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            Y           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            Z           1262    49166    MHW-API    DATABASE     |   CREATE DATABASE "MHW-API" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE "MHW-API";
                postgres    false            �            1259    65945    biomas    TABLE     f   CREATE TABLE public.biomas (
    id_bioma integer NOT NULL,
    nombre_bioma character varying(50)
);
    DROP TABLE public.biomas;
       public         heap    postgres    false            �            1259    65944    biomas_id_bioma_seq    SEQUENCE     �   CREATE SEQUENCE public.biomas_id_bioma_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.biomas_id_bioma_seq;
       public          postgres    false    228            [           0    0    biomas_id_bioma_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.biomas_id_bioma_seq OWNED BY public.biomas.id_bioma;
          public          postgres    false    227            �            1259    65907    categoria_monstro    TABLE     p   CREATE TABLE public.categoria_monstro (
    id_tipo_monstro integer NOT NULL,
    tipo character varying(20)
);
 %   DROP TABLE public.categoria_monstro;
       public         heap    postgres    false            �            1259    65906 %   categoria_monstro_id_tipo_monstro_seq    SEQUENCE     �   CREATE SEQUENCE public.categoria_monstro_id_tipo_monstro_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public.categoria_monstro_id_tipo_monstro_seq;
       public          postgres    false    220            \           0    0 %   categoria_monstro_id_tipo_monstro_seq    SEQUENCE OWNED BY     o   ALTER SEQUENCE public.categoria_monstro_id_tipo_monstro_seq OWNED BY public.categoria_monstro.id_tipo_monstro;
          public          postgres    false    219            �            1259    49999    debilidades    TABLE     �   CREATE TABLE public.debilidades (
    id integer NOT NULL,
    id_elemento integer,
    id_monstro integer,
    eficacia double precision
);
    DROP TABLE public.debilidades;
       public         heap    postgres    false            �            1259    49998    debilidades_id_seq    SEQUENCE     �   CREATE SEQUENCE public.debilidades_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.debilidades_id_seq;
       public          postgres    false    216            ]           0    0    debilidades_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.debilidades_id_seq OWNED BY public.debilidades.id;
          public          postgres    false    215            �            1259    66003    elemento_monstro    TABLE     l   CREATE TABLE public.elemento_monstro (
    id_elemento integer NOT NULL,
    id_monstro integer NOT NULL
);
 $   DROP TABLE public.elemento_monstro;
       public         heap    postgres    false            �            1259    65952 	   elementos    TABLE     h   CREATE TABLE public.elementos (
    id_elemento integer NOT NULL,
    elemento character varying(20)
);
    DROP TABLE public.elementos;
       public         heap    postgres    false            �            1259    65951    elementos_id_elemento_seq    SEQUENCE     �   CREATE SEQUENCE public.elementos_id_elemento_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.elementos_id_elemento_seq;
       public          postgres    false    230            ^           0    0    elementos_id_elemento_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.elementos_id_elemento_seq OWNED BY public.elementos.id_elemento;
          public          postgres    false    229            �            1259    65926    items    TABLE     �   CREATE TABLE public.items (
    id_item integer NOT NULL,
    id_monstro integer,
    nombre_item character varying(50),
    descripcion_item character varying(255)
);
    DROP TABLE public.items;
       public         heap    postgres    false            �            1259    65925    items_id_item_seq    SEQUENCE     �   CREATE SEQUENCE public.items_id_item_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.items_id_item_seq;
       public          postgres    false    224            _           0    0    items_id_item_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.items_id_item_seq OWNED BY public.items.id_item;
          public          postgres    false    223            �            1259    65973    mg_bioma    TABLE     a   CREATE TABLE public.mg_bioma (
    id_bioma integer NOT NULL,
    id_monstro integer NOT NULL
);
    DROP TABLE public.mg_bioma;
       public         heap    postgres    false            �            1259    65988    mg_debilidades    TABLE     �   CREATE TABLE public.mg_debilidades (
    id_elemento integer NOT NULL,
    id_monstro integer NOT NULL,
    eficacia double precision
);
 "   DROP TABLE public.mg_debilidades;
       public         heap    postgres    false            �            1259    57457    mg_elemento    TABLE     n   CREATE TABLE public.mg_elemento (
    id integer NOT NULL,
    id_elemento integer,
    id_monstro integer
);
    DROP TABLE public.mg_elemento;
       public         heap    postgres    false            �            1259    57456    mg_elemento_id_seq    SEQUENCE     �   CREATE SEQUENCE public.mg_elemento_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.mg_elemento_id_seq;
       public          postgres    false    218            `           0    0    mg_elemento_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.mg_elemento_id_seq OWNED BY public.mg_elemento.id;
          public          postgres    false    217            �            1259    65958    mg_rango    TABLE     a   CREATE TABLE public.mg_rango (
    id_rango integer NOT NULL,
    id_monstro integer NOT NULL
);
    DROP TABLE public.mg_rango;
       public         heap    postgres    false            �            1259    65914    monstro_grande    TABLE     �   CREATE TABLE public.monstro_grande (
    id_monstrog integer NOT NULL,
    nombre character varying(20),
    vida integer,
    id_categoria integer
);
 "   DROP TABLE public.monstro_grande;
       public         heap    postgres    false            �            1259    65913    monstro_grande_id_monstrog_seq    SEQUENCE     �   CREATE SEQUENCE public.monstro_grande_id_monstrog_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 5   DROP SEQUENCE public.monstro_grande_id_monstrog_seq;
       public          postgres    false    222            a           0    0    monstro_grande_id_monstrog_seq    SEQUENCE OWNED BY     a   ALTER SEQUENCE public.monstro_grande_id_monstrog_seq OWNED BY public.monstro_grande.id_monstrog;
          public          postgres    false    221            �            1259    65938    rangos    TABLE     _   CREATE TABLE public.rangos (
    id_rango integer NOT NULL,
    rango character varying(20)
);
    DROP TABLE public.rangos;
       public         heap    postgres    false            �            1259    65937    rangos_id_rango_seq    SEQUENCE     �   CREATE SEQUENCE public.rangos_id_rango_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.rangos_id_rango_seq;
       public          postgres    false    226            b           0    0    rangos_id_rango_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.rangos_id_rango_seq OWNED BY public.rangos.id_rango;
          public          postgres    false    225            �            1259    66018    v_monstro_grande    VIEW     %  CREATE VIEW public.v_monstro_grande AS
 SELECT monstro_grande.id_monstrog,
    monstro_grande.nombre,
    monstro_grande.vida,
    categoria_monstro.tipo
   FROM (public.monstro_grande
     JOIN public.categoria_monstro ON ((monstro_grande.id_categoria = categoria_monstro.id_tipo_monstro)));
 #   DROP VIEW public.v_monstro_grande;
       public          postgres    false    220    220    222    222    222    222            �           2604    65948    biomas id_bioma    DEFAULT     r   ALTER TABLE ONLY public.biomas ALTER COLUMN id_bioma SET DEFAULT nextval('public.biomas_id_bioma_seq'::regclass);
 >   ALTER TABLE public.biomas ALTER COLUMN id_bioma DROP DEFAULT;
       public          postgres    false    228    227    228            �           2604    65910 !   categoria_monstro id_tipo_monstro    DEFAULT     �   ALTER TABLE ONLY public.categoria_monstro ALTER COLUMN id_tipo_monstro SET DEFAULT nextval('public.categoria_monstro_id_tipo_monstro_seq'::regclass);
 P   ALTER TABLE public.categoria_monstro ALTER COLUMN id_tipo_monstro DROP DEFAULT;
       public          postgres    false    219    220    220            �           2604    50002    debilidades id    DEFAULT     p   ALTER TABLE ONLY public.debilidades ALTER COLUMN id SET DEFAULT nextval('public.debilidades_id_seq'::regclass);
 =   ALTER TABLE public.debilidades ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    216    215    216            �           2604    65955    elementos id_elemento    DEFAULT     ~   ALTER TABLE ONLY public.elementos ALTER COLUMN id_elemento SET DEFAULT nextval('public.elementos_id_elemento_seq'::regclass);
 D   ALTER TABLE public.elementos ALTER COLUMN id_elemento DROP DEFAULT;
       public          postgres    false    230    229    230            �           2604    65929    items id_item    DEFAULT     n   ALTER TABLE ONLY public.items ALTER COLUMN id_item SET DEFAULT nextval('public.items_id_item_seq'::regclass);
 <   ALTER TABLE public.items ALTER COLUMN id_item DROP DEFAULT;
       public          postgres    false    224    223    224            �           2604    57460    mg_elemento id    DEFAULT     p   ALTER TABLE ONLY public.mg_elemento ALTER COLUMN id SET DEFAULT nextval('public.mg_elemento_id_seq'::regclass);
 =   ALTER TABLE public.mg_elemento ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    217    218            �           2604    65917    monstro_grande id_monstrog    DEFAULT     �   ALTER TABLE ONLY public.monstro_grande ALTER COLUMN id_monstrog SET DEFAULT nextval('public.monstro_grande_id_monstrog_seq'::regclass);
 I   ALTER TABLE public.monstro_grande ALTER COLUMN id_monstrog DROP DEFAULT;
       public          postgres    false    222    221    222            �           2604    65941    rangos id_rango    DEFAULT     r   ALTER TABLE ONLY public.rangos ALTER COLUMN id_rango SET DEFAULT nextval('public.rangos_id_rango_seq'::regclass);
 >   ALTER TABLE public.rangos ALTER COLUMN id_rango DROP DEFAULT;
       public          postgres    false    225    226    226            N          0    65945    biomas 
   TABLE DATA           8   COPY public.biomas (id_bioma, nombre_bioma) FROM stdin;
    public          postgres    false    228   �`       F          0    65907    categoria_monstro 
   TABLE DATA           B   COPY public.categoria_monstro (id_tipo_monstro, tipo) FROM stdin;
    public          postgres    false    220   a       B          0    49999    debilidades 
   TABLE DATA           L   COPY public.debilidades (id, id_elemento, id_monstro, eficacia) FROM stdin;
    public          postgres    false    216   �a       T          0    66003    elemento_monstro 
   TABLE DATA           C   COPY public.elemento_monstro (id_elemento, id_monstro) FROM stdin;
    public          postgres    false    234   �a       P          0    65952 	   elementos 
   TABLE DATA           :   COPY public.elementos (id_elemento, elemento) FROM stdin;
    public          postgres    false    230   �a       J          0    65926    items 
   TABLE DATA           S   COPY public.items (id_item, id_monstro, nombre_item, descripcion_item) FROM stdin;
    public          postgres    false    224   Hb       R          0    65973    mg_bioma 
   TABLE DATA           8   COPY public.mg_bioma (id_bioma, id_monstro) FROM stdin;
    public          postgres    false    232   �b       S          0    65988    mg_debilidades 
   TABLE DATA           K   COPY public.mg_debilidades (id_elemento, id_monstro, eficacia) FROM stdin;
    public          postgres    false    233   �b       D          0    57457    mg_elemento 
   TABLE DATA           B   COPY public.mg_elemento (id, id_elemento, id_monstro) FROM stdin;
    public          postgres    false    218   c       Q          0    65958    mg_rango 
   TABLE DATA           8   COPY public.mg_rango (id_rango, id_monstro) FROM stdin;
    public          postgres    false    231   0c       H          0    65914    monstro_grande 
   TABLE DATA           Q   COPY public.monstro_grande (id_monstrog, nombre, vida, id_categoria) FROM stdin;
    public          postgres    false    222   _c       L          0    65938    rangos 
   TABLE DATA           1   COPY public.rangos (id_rango, rango) FROM stdin;
    public          postgres    false    226   �c       c           0    0    biomas_id_bioma_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.biomas_id_bioma_seq', 6, true);
          public          postgres    false    227            d           0    0 %   categoria_monstro_id_tipo_monstro_seq    SEQUENCE SET     S   SELECT pg_catalog.setval('public.categoria_monstro_id_tipo_monstro_seq', 8, true);
          public          postgres    false    219            e           0    0    debilidades_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.debilidades_id_seq', 6, true);
          public          postgres    false    215            f           0    0    elementos_id_elemento_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.elementos_id_elemento_seq', 6, true);
          public          postgres    false    229            g           0    0    items_id_item_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.items_id_item_seq', 13, true);
          public          postgres    false    223            h           0    0    mg_elemento_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.mg_elemento_id_seq', 2, true);
          public          postgres    false    217            i           0    0    monstro_grande_id_monstrog_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public.monstro_grande_id_monstrog_seq', 2, true);
          public          postgres    false    221            j           0    0    rangos_id_rango_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.rangos_id_rango_seq', 3, true);
          public          postgres    false    225            �           2606    65950    biomas biomas_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.biomas
    ADD CONSTRAINT biomas_pkey PRIMARY KEY (id_bioma);
 <   ALTER TABLE ONLY public.biomas DROP CONSTRAINT biomas_pkey;
       public            postgres    false    228            �           2606    65912 (   categoria_monstro categoria_monstro_pkey 
   CONSTRAINT     s   ALTER TABLE ONLY public.categoria_monstro
    ADD CONSTRAINT categoria_monstro_pkey PRIMARY KEY (id_tipo_monstro);
 R   ALTER TABLE ONLY public.categoria_monstro DROP CONSTRAINT categoria_monstro_pkey;
       public            postgres    false    220            �           2606    50004    debilidades debilidades_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.debilidades
    ADD CONSTRAINT debilidades_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.debilidades DROP CONSTRAINT debilidades_pkey;
       public            postgres    false    216            �           2606    66007 &   elemento_monstro elemento_monstro_pkey 
   CONSTRAINT     y   ALTER TABLE ONLY public.elemento_monstro
    ADD CONSTRAINT elemento_monstro_pkey PRIMARY KEY (id_elemento, id_monstro);
 P   ALTER TABLE ONLY public.elemento_monstro DROP CONSTRAINT elemento_monstro_pkey;
       public            postgres    false    234    234            �           2606    65957    elementos elementos_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public.elementos
    ADD CONSTRAINT elementos_pkey PRIMARY KEY (id_elemento);
 B   ALTER TABLE ONLY public.elementos DROP CONSTRAINT elementos_pkey;
       public            postgres    false    230            �           2606    65931    items items_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.items
    ADD CONSTRAINT items_pkey PRIMARY KEY (id_item);
 :   ALTER TABLE ONLY public.items DROP CONSTRAINT items_pkey;
       public            postgres    false    224            �           2606    65977    mg_bioma mg_bioma_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.mg_bioma
    ADD CONSTRAINT mg_bioma_pkey PRIMARY KEY (id_bioma, id_monstro);
 @   ALTER TABLE ONLY public.mg_bioma DROP CONSTRAINT mg_bioma_pkey;
       public            postgres    false    232    232            �           2606    65992 "   mg_debilidades mg_debilidades_pkey 
   CONSTRAINT     u   ALTER TABLE ONLY public.mg_debilidades
    ADD CONSTRAINT mg_debilidades_pkey PRIMARY KEY (id_elemento, id_monstro);
 L   ALTER TABLE ONLY public.mg_debilidades DROP CONSTRAINT mg_debilidades_pkey;
       public            postgres    false    233    233            �           2606    57462    mg_elemento mg_elemento_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.mg_elemento
    ADD CONSTRAINT mg_elemento_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.mg_elemento DROP CONSTRAINT mg_elemento_pkey;
       public            postgres    false    218            �           2606    65962    mg_rango mg_rango_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.mg_rango
    ADD CONSTRAINT mg_rango_pkey PRIMARY KEY (id_rango, id_monstro);
 @   ALTER TABLE ONLY public.mg_rango DROP CONSTRAINT mg_rango_pkey;
       public            postgres    false    231    231            �           2606    65919 "   monstro_grande monstro_grande_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.monstro_grande
    ADD CONSTRAINT monstro_grande_pkey PRIMARY KEY (id_monstrog);
 L   ALTER TABLE ONLY public.monstro_grande DROP CONSTRAINT monstro_grande_pkey;
       public            postgres    false    222            �           2606    65943    rangos rangos_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.rangos
    ADD CONSTRAINT rangos_pkey PRIMARY KEY (id_rango);
 <   ALTER TABLE ONLY public.rangos DROP CONSTRAINT rangos_pkey;
       public            postgres    false    226            �           2606    65920    monstro_grande fk_categoria    FK CONSTRAINT     �   ALTER TABLE ONLY public.monstro_grande
    ADD CONSTRAINT fk_categoria FOREIGN KEY (id_categoria) REFERENCES public.categoria_monstro(id_tipo_monstro) ON UPDATE CASCADE ON DELETE CASCADE;
 E   ALTER TABLE ONLY public.monstro_grande DROP CONSTRAINT fk_categoria;
       public          postgres    false    220    4756    222            �           2606    65978    mg_bioma fk_idbioma    FK CONSTRAINT     �   ALTER TABLE ONLY public.mg_bioma
    ADD CONSTRAINT fk_idbioma FOREIGN KEY (id_bioma) REFERENCES public.biomas(id_bioma) ON UPDATE CASCADE ON DELETE CASCADE;
 =   ALTER TABLE ONLY public.mg_bioma DROP CONSTRAINT fk_idbioma;
       public          postgres    false    232    228    4764            �           2606    66008    elemento_monstro fk_idelemento    FK CONSTRAINT     �   ALTER TABLE ONLY public.elemento_monstro
    ADD CONSTRAINT fk_idelemento FOREIGN KEY (id_elemento) REFERENCES public.elementos(id_elemento) ON UPDATE CASCADE ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.elemento_monstro DROP CONSTRAINT fk_idelemento;
       public          postgres    false    230    4766    234            �           2606    65993    mg_debilidades fk_idmelemento    FK CONSTRAINT     �   ALTER TABLE ONLY public.mg_debilidades
    ADD CONSTRAINT fk_idmelemento FOREIGN KEY (id_elemento) REFERENCES public.elementos(id_elemento) ON UPDATE CASCADE ON DELETE CASCADE;
 G   ALTER TABLE ONLY public.mg_debilidades DROP CONSTRAINT fk_idmelemento;
       public          postgres    false    4766    233    230            �           2606    65968    mg_rango fk_idmonstro    FK CONSTRAINT     �   ALTER TABLE ONLY public.mg_rango
    ADD CONSTRAINT fk_idmonstro FOREIGN KEY (id_monstro) REFERENCES public.monstro_grande(id_monstrog) ON UPDATE CASCADE ON DELETE CASCADE;
 ?   ALTER TABLE ONLY public.mg_rango DROP CONSTRAINT fk_idmonstro;
       public          postgres    false    231    4758    222            �           2606    65983    mg_bioma fk_idmonstro    FK CONSTRAINT     �   ALTER TABLE ONLY public.mg_bioma
    ADD CONSTRAINT fk_idmonstro FOREIGN KEY (id_monstro) REFERENCES public.monstro_grande(id_monstrog) ON UPDATE CASCADE ON DELETE CASCADE;
 ?   ALTER TABLE ONLY public.mg_bioma DROP CONSTRAINT fk_idmonstro;
       public          postgres    false    222    4758    232            �           2606    65998    mg_debilidades fk_idmonstro    FK CONSTRAINT     �   ALTER TABLE ONLY public.mg_debilidades
    ADD CONSTRAINT fk_idmonstro FOREIGN KEY (id_monstro) REFERENCES public.monstro_grande(id_monstrog) ON UPDATE CASCADE ON DELETE CASCADE;
 E   ALTER TABLE ONLY public.mg_debilidades DROP CONSTRAINT fk_idmonstro;
       public          postgres    false    222    233    4758            �           2606    66013    elemento_monstro fk_idmonstro    FK CONSTRAINT     �   ALTER TABLE ONLY public.elemento_monstro
    ADD CONSTRAINT fk_idmonstro FOREIGN KEY (id_monstro) REFERENCES public.monstro_grande(id_monstrog) ON UPDATE CASCADE ON DELETE CASCADE;
 G   ALTER TABLE ONLY public.elemento_monstro DROP CONSTRAINT fk_idmonstro;
       public          postgres    false    222    234    4758            �           2606    65963    mg_rango fk_idrango    FK CONSTRAINT     �   ALTER TABLE ONLY public.mg_rango
    ADD CONSTRAINT fk_idrango FOREIGN KEY (id_rango) REFERENCES public.rangos(id_rango) ON UPDATE CASCADE ON DELETE CASCADE;
 =   ALTER TABLE ONLY public.mg_rango DROP CONSTRAINT fk_idrango;
       public          postgres    false    4762    226    231            �           2606    65932    items fk_items    FK CONSTRAINT     �   ALTER TABLE ONLY public.items
    ADD CONSTRAINT fk_items FOREIGN KEY (id_monstro) REFERENCES public.monstro_grande(id_monstrog) ON UPDATE CASCADE ON DELETE CASCADE;
 8   ALTER TABLE ONLY public.items DROP CONSTRAINT fk_items;
       public          postgres    false    4758    222    224            N   s   x��;�@ ��>Ş �ojj$$˘��Y{���@ꙷ���KYBg���n������0X�Ũza2��x�+��ho!O��x���R�,TY鿞`���$��'���(6*�      F   r   x�U�M
�0��u�9�������f4���y:F��o�p�~��_�]��ϲl���{}w1��mt+sG� �{�p@] �����qOYs���3��u��o=�u��]��018      B   0   x�3�4�4�4�2���F\Ɯ&`ڄ���LA< ߌ,����� �bB      T      x�3�4�2�4����� �      P   >   x�3�t+MM��2�tL/M�2�)J-���2���L���2�t)JL���2����KI��qqq �7k      J   U   x�3�4��,I�u�J�-���LI-N.�,H����
s�7& oB@ޔ��ys��Fx�-	�R`HH�!���qqq �͒m      R      x�3�4�2bCN#�=... 
      S   #   x�3�4�4�2�F\&`҈�,b�1z\\\ fo�      D      x�3�4�4�2�4�4����� `      Q      x�3�4�2bc 6�4��lC�=... 4hp      H   4   x�3�(�N�����&��\F�!�I���)����F��F��\1z\\\ A��      L   ,   x�3�J�K�WHJ���2�rsJ򹌡����⒢|�=... Z     